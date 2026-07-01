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

            decimal? cc = dangKyHocPhan.DiemChuyenCan;
            decimal? gk = dangKyHocPhan.DiemGiuaKy;
            decimal? ck = dangKyHocPhan.DiemCuoiKy;

            if (cc < 0 || cc > 10 || gk < 0 || gk > 10 || ck < 0 || ck > 10)
            {
                return BadRequest("Điểm phải nằm trong khoảng từ 0 đến 10.");
            }

            if (cc.HasValue && gk.HasValue && ck.HasValue)
            {
                dangKyHocPhan.DiemTongKet = Math.Round((cc.Value * 0.1m + gk.Value * 0.3m + ck.Value * 0.6m), 2);
                if (dangKyHocPhan.DiemTongKet >= 8.5m)
                {
                    dangKyHocPhan.XepLoai = "Giỏi";
                }
                else if (dangKyHocPhan.DiemTongKet >= 7.0m)
                {
                    dangKyHocPhan.XepLoai = "Khá";
                }
                else if (dangKyHocPhan.DiemTongKet >= 5.0m)
                {
                    dangKyHocPhan.XepLoai = "Trung bình";
                }
                else
                {
                    dangKyHocPhan.XepLoai = "Yếu";
                }
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
