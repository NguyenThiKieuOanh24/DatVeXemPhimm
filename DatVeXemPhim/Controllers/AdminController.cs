using Microsoft.AspNetCore.Mvc;

namespace DatVeXemPhim.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DangNhap()
        {
            return View();
        }
    }
}
