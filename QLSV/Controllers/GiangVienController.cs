using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLSV.Models;

namespace QLSV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiangVienController : ControllerBase
    {
        private readonly QLSVContext _db;

        public GiangVienController(QLSVContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAllGiangViens([FromQuery] bool includeInactive = false)
        {
            var query = _db.GiangViens.AsQueryable(); // Tạo query rỗng để nối được chuỗi truy vấn
            if (!includeInactive)
                query = query.Where(gv => gv.TrangThai == true || gv.TrangThai == null); // Lọc giảng viên đang hoạt động

            return Ok(query.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetGiangVienById(string id)
        {
            var gv = _db.GiangViens.Find(id);
            return Ok(gv);
        }

        [HttpPost]
        public IActionResult CreateGiangVien(GiangVien giangVien)
        {
            _db.GiangViens.Add(giangVien);
            _db.SaveChanges();
            return CreatedAtAction(nameof(GetGiangVienById), new { id = giangVien.MaGv }, giangVien);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGiangVien(string id, GiangVien giangVien)
        {
            var gv = _db.GiangViens.Find(id);
            if (gv == null)
            {
                return NotFound();
            }
            gv.MaKhoa = giangVien.MaKhoa;
            gv.TenGiangVien = giangVien.TenGiangVien;
            gv.Sdt = giangVien.Sdt;
            gv.Email = giangVien.Email;
            gv.GioiTinh = giangVien.GioiTinh;
            gv.NgayVaoTruong = giangVien.NgayVaoTruong;
            gv.TrangThai = giangVien.TrangThai;
            _db.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGiangVien(string id)
        {
            var gv = _db.GiangViens.Find(id);
            if (gv == null)
            {
                return NotFound();
            }

            // Soft delete
            gv.TrangThai = false;
            _db.SaveChanges();

            return NoContent();
        }

        // Endpoint phục hồi giảng viên đã bị soft delete
        [HttpPatch("{id}/restore")]
        public IActionResult RestoreGiangVien(string id)
        {
            var gv = _db.GiangViens.Find(id);
            if (gv == null)
                return NotFound();

            gv.TrangThai = true;
            _db.SaveChanges();
            return NoContent();
        }
    }
}
