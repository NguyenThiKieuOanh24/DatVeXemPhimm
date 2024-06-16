using DatVeXemPhim.App_Start;
using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DatVeXemPhim.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DatVeXemPhimContext _context;

        public HomeController(ILogger<HomeController> logger, DatVeXemPhimContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()    
        {
            return View(await _context.Phim.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Dangnhap()
        {
            return View();
        }

        public IActionResult Phim()
        {
            return View();
        }

        public async Task<IActionResult> Search(string searchString)
        {

            ViewBag.SearchString = searchString;

            if (_context.Phim == null)
            {
                return Problem("Danh sách trống");
            }

            var movies = from m in _context.Phim
                         select m;
            var k = movies.Count();
            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.tenPhim!.Contains(searchString));  
            }
            var a = 5;
            return View(await movies.ToListAsync());
        }
        public IActionResult Ve()
        {
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phim = await _context.Phim
                .Include(p => p.fk_TheLoaiPhim)
                .FirstOrDefaultAsync(m => m.id == id);
            if (phim == null)
            {
                return NotFound();
            }
            await _context.SaveChangesAsync();
            return View(phim);
        }

        public async Task<IActionResult> VeDaDat()
        {
            var user = SessionConfig.GetKhachHang();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }


            int userId = user.id;
            var dsve = await _context.Ve
                .Where(v => v.maKhachHang == userId)
                .Include(p => p.fk_MaGhe)
                .Include(p => p.fk_XuatChieu)
                .ThenInclude(x => x.fk_Phim)
                .Include(p => p.fk_MaGhe)
                .ThenInclude(g => g.fk_PhongChieu)
                .ToListAsync();
            
            if (dsve == null || dsve.Count == 0)
            {
                return View();
            }
            return View(dsve);
        }
    }
}
