using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLSV.Models;

namespace QLSV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LopHocController : ControllerBase
    {
        private readonly QLSVContext _db;

        public LopHocController(QLSVContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAllLopHocs()
        {
            var ds = _db.LopHocs.ToList();
            return Ok(ds);
        }

        [HttpGet("{id}")]
        public IActionResult GetLopHocById(string id)
        {
            var lh = _db.LopHocs.Find(id);
            return Ok(lh);
        }

        [HttpPost]
        public IActionResult CreateLopHoc(LopHoc lopHoc)
        {
            _db.LopHocs.Add(lopHoc);
            _db.SaveChanges();
            return CreatedAtAction(nameof(GetLopHocById), new { id = lopHoc.MaLop }, lopHoc);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLopHoc(string id, LopHoc lopHoc)
        {
            var lh = _db.LopHocs.Find(id);
            if (lh == null)
            {
                return NotFound();
            }
            lh.MaKhoa = lopHoc.MaKhoa;
            lh.MaGv = lopHoc.MaGv;
            lh.TenLop = lopHoc.TenLop;
            lh.KhoaHoc = lopHoc.KhoaHoc;
            _db.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLopHoc(string id)
        {
            var lh = _db.LopHocs.Find(id);
            if (lh == null)
            {
                return NotFound();
            }
            _db.LopHocs.Remove(lh);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
