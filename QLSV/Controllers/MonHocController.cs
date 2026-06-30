using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLSV.Models;

namespace QLSV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonHocController : ControllerBase
    {
        private readonly QLSVContext _db;

        public MonHocController(QLSVContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAllMonHocs()
        {
            var ds = _db.MonHocs.ToList();
            return Ok(ds);
        }

        [HttpGet("{id}")]
        public IActionResult GetMonHocById(string id)
        {
            var mh = _db.MonHocs.Find(id);
            return Ok(mh);
        }

        [HttpPost]
        public IActionResult CreateMonHoc(MonHoc monHoc)
        {
            _db.MonHocs.Add(monHoc);
            _db.SaveChanges();
            return CreatedAtAction(nameof(GetMonHocById), new { id = monHoc.MaMh }, monHoc);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMonHoc(string id, MonHoc monHoc)
        {
            var mh = _db.MonHocs.Find(id);
            if (mh == null)
            {
                return NotFound();
            }
            mh.MaKhoa = monHoc.MaKhoa;
            mh.TenMh = monHoc.TenMh;
            mh.SoTinChi = monHoc.SoTinChi;
            _db.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMonHoc(string id)
        {
            var mh = _db.MonHocs.Find(id);
            if (mh == null)
            {
                return NotFound();
            }
            _db.MonHocs.Remove(mh);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
