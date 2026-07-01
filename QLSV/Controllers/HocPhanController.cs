using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLSV.Models;

namespace QLSV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HocPhanController : ControllerBase
    {
        private readonly QLSVContext _db;

        public HocPhanController(QLSVContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAllHocPhans()
        {
            var ds = _db.HocPhans.ToList();
            return Ok(ds);
        }

        [HttpGet("{id}")]
        public IActionResult GetHocPhanById(string id)
        {
            var hp = _db.HocPhans.Find(id);
            if (hp == null)
            {
                return NotFound();
            }
            return Ok(hp);
        }

        [HttpPost]
        public IActionResult CreateHocPhan(HocPhan hocPhan)
        {
            _db.HocPhans.Add(hocPhan);
            _db.SaveChanges();
            return CreatedAtAction(nameof(GetHocPhanById), new { id = hocPhan.MaHp }, hocPhan);
        }

        [HttpPut]
        public IActionResult UpdateHocPhan (string id,  HocPhan hocPhan)
        {
            var hp = _db.HocPhans.Find(id);
            if (hp == null)
            {
                return NotFound();
            }
            hp.MaMh = hocPhan.MaMh;
            hp.MaGv = hocPhan.MaGv;
            hp.HocKy = hocPhan.HocKy;
            hp.NamHoc = hocPhan.NamHoc;
            hp.SiSoToiDa = hocPhan.SiSoToiDa;
            hp.PhongHoc = hocPhan.PhongHoc;
            hp.LichHoc = hocPhan.LichHoc;
            _db.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteHocPhan(string id)
        {
            var hp = _db.HocPhans.Find(id);
            if (hp == null)
            {
                return NotFound();
            }
            _db.HocPhans.Remove(hp);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
