using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLSV.Models;

namespace QLSV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SinhVienController : ControllerBase
    {
        private readonly QLSVContext _db;

        public SinhVienController(QLSVContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAllSinhViens()
        {
            var ds = _db.SinhViens.ToList();
            return Ok(ds);
        }

        [HttpGet("{id}")]
        public IActionResult GetSinhVienById(string id)
        {
            var sv = _db.SinhViens.Find(id);
            return Ok(sv);
        }

        [HttpPost]
        public IActionResult CreateSinhVien([FromBody] SinhVien sinhVien)
        {
            _db.SinhViens.Add(sinhVien);
            _db.SaveChanges();
            return CreatedAtAction(nameof(GetSinhVienById),new { id = sinhVien.MaSv }, sinhVien);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSinhVien(string id, [FromBody] SinhVien sinhVien)
        {
            var existingSinhVien = _db.SinhViens.Find(id);
            if (existingSinhVien == null)
            {
                return NotFound();
            }
            existingSinhVien.HoTen = sinhVien.HoTen;
            existingSinhVien.NgayThangNamSinh = sinhVien.NgayThangNamSinh;
            existingSinhVien.GioiTinh = sinhVien.GioiTinh;
            existingSinhVien.QueQuan = sinhVien.QueQuan;
            existingSinhVien.MaLop = sinhVien.MaLop;
            existingSinhVien.Sdt = sinhVien.Sdt;
            existingSinhVien.Email = sinhVien.Email;
            existingSinhVien.NgayNhapHoc = sinhVien.NgayNhapHoc;
            existingSinhVien.TrangThai = sinhVien.TrangThai;
            _db.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteSinhVien(string id)
        {
            var existingSinhVien = _db.SinhViens.Find(id);
            if (existingSinhVien == null)
            {
                return NotFound();
            }
            _db.SinhViens.Remove(existingSinhVien);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
