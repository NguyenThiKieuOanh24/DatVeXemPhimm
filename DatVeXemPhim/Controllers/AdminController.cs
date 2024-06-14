using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers
{
    public class AdminController : Controller
    {
        private readonly DatVeXemPhimContext _context;

        public AdminController(DatVeXemPhimContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DangNhap(NhanVien model)
        {

            var user = await _context.NhanVien.FirstOrDefaultAsync(u => u.taiKhoan == model.taiKhoan && u.matKhau == model.matKhau);

            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.id.ToString());
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không hợp lệ");
            }
            return View(model);
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
