using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
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
            ViewBag.CurrentDate = DateTime.Now;
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

            if (_context.Phim == null)
            {
                return Problem("List null");
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
    }
}
