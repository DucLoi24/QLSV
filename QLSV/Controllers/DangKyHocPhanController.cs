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

        [HttpGet("{maHp}/{maSv}")]
        public IActionResult GetDangKyHocPhanById(string maHp, string maSv)
        {
            var dk = _db.DangKyHocPhans.Find(new object[] {maHp, maSv});
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
            return CreatedAtAction(nameof(GetDangKyHocPhanById), new { maHp = dangKyHocPhan.MaHp, maSv = dangKyHocPhan.MaSv }, dangKyHocPhan);
        }

        [HttpPut("{maHp}/{maSv}")]
        public IActionResult UpdateDangKyHocPhan(string maHp, string maSv, DangKyHocPhan dangKyHocPhan)
        {
            var dkhp = _db.DangKyHocPhans.Find(new object[] {maHp, maSv});
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

        [HttpDelete("{maHp}/{maSv}")]
        public IActionResult DeleteDangKyHocPhan(string maHp, string maSv)
        {
            var dkhp = _db.DangKyHocPhans.Find(new object[] {maHp, maSv});
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
