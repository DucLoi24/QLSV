using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLSV.Models;

namespace QLSV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhoaController : ControllerBase
    {
        private readonly QLSVContext _db;

        public KhoaController(QLSVContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAllKhoas()
        {
            var ds = _db.Khoas.ToList();
            return Ok(ds);
        }

        [HttpGet("{id}")]
        public IActionResult GetKhoaById(string id)
        {
            var khoa = _db.Khoas.Find(id);
            return Ok(khoa);
        }

        [HttpPost]
        public IActionResult CreateKhoa(Khoa khoa)
        {
            _db.Khoas.Add(khoa);
            _db.SaveChanges();
            return CreatedAtAction(nameof(GetKhoaById), new { id = khoa.MaKhoa }, khoa);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateKhoa(string id, Khoa khoa)
        {
            var kh = _db.Khoas.Find(id);
            if (kh == null)
            {
                return NotFound();
            }
            kh.TenKhoa = khoa.TenKhoa;
            kh.MaTruongKhoa = khoa.MaTruongKhoa;
            _db.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteKhoa(string id)
        {
            var kh = _db.Khoas.Find(id);
            if (kh == null)
            {
                return NotFound();
            }
            _db.Khoas.Remove(kh);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
