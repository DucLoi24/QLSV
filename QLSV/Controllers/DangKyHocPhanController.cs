using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLSV.Models;

namespace QLSV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DangKyHocPhanController : ControllerBase
    {
        private readonly QLSVContext _db;

        public DangKyHocPhanController(QLSVContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAllDangKyHocPhans()
        {
            var ds = _db.DangKyHocPhans.ToList();
            return Ok(ds);
        }

        [HttpGet("{id}")]
        public IActionResult GetDangKyHocPhanById(string id)
        {
            var dk = _db.DangKyHocPhans.Find(id);
            if (dk == null)
            {
                return NotFound();
            }
            return Ok(dk);
        }

        [HttpPost]
        public IActionResult CreateDangKyHocPhan(DangKyHocPhan dangKyHocPhan)
        {
            _db.Add(dangKyHocPhan);
            _db.SaveChanges();
            return CreatedAtAction(nameof(GetDangKyHocPhanById), new { id = dangKyHocPhan.MaHp, dangKyHocPhan });
        }

        [HttpPut]
        public IActionResult UpdateDangKyHocPhan(string id, DangKyHocPhan dangKyHocPhan)
        {
            var dkhp = _db.DangKyHocPhans.Find(id);
            if (dkhp == null)
            {
                return NotFound();
            }
            dkhp.NgayDangKy = dangKyHocPhan.NgayDangKy;
            dkhp.TrangThai = dangKyHocPhan.TrangThai;
            dkhp.DiemChuyenCan = dangKyHocPhan.DiemChuyenCan;
            dkhp.DiemGiuaKy = dangKyHocPhan.DiemGiuaKy;
            dkhp.DiemCuoiKy = dangKyHocPhan.DiemCuoiKy;
            dkhp.DiemTongKet = dangKyHocPhan.DiemTongKet;
            dkhp.XepLoai = dangKyHocPhan.XepLoai;
            _db.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteDangKyHocPhan(string id)
        {
            var dkhp = _db.DangKyHocPhans.Find(id);
            if (dkhp == null)
            {
                return NotFound();
            }
            _db.DangKyHocPhans.Remove(dkhp);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
