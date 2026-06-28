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
        public IActionResult CreateSinhVien(SinhVien sinhVien)
        {
            _db.SinhViens.Add(sinhVien);
            _db.SaveChanges();
            return CreatedAtAction(nameof(GetSinhVienById),new { id = sinhVien.MaSv }, sinhVien);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSinhVien(string id, SinhVien sinhVien)
        {
            var sv = _db.SinhViens.Find(id);
            if (sv == null)
            {
                return NotFound();
            }
            sv.HoTen = sinhVien.HoTen;
            sv.NgayThangNamSinh = sinhVien.NgayThangNamSinh;
            sv.GioiTinh = sinhVien.GioiTinh;
            sv.QueQuan = sinhVien.QueQuan;
            sv.MaLop = sinhVien.MaLop;
            sv.Sdt = sinhVien.Sdt;
            sv.Email = sinhVien.Email;
            sv.NgayNhapHoc = sinhVien.NgayNhapHoc;
            sv.TrangThai = sinhVien.TrangThai;
            _db.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSinhVien(string id)
        {
            var sv = _db.SinhViens.Find(id);
            if (sv == null)
            {
                return NotFound();
            }
            _db.SinhViens.Remove(sv);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
