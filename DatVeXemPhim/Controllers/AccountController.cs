using Microsoft.AspNetCore.Mvc;
using DatVeXemPhim.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using DatVeXemPhim.Data;
using Microsoft.AspNetCore.Authentication;

namespace DatVeXemPhim.Controllers
{
    public class AccountController : Controller
    {
        private readonly DatVeXemPhimContext _context;

        public AccountController(DatVeXemPhimContext context)
        {
            _context = context;
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("id,hoTen,soDienThoai,email,taiKhoan,matKhau")] KhachHang khachHang, string confirmPassword)
        {
            if (ModelState.IsValid)
            {
                if (khachHang.matKhau != confirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Mật khẩu xác nhận không khớp.");
                    return View(khachHang);
                }

                // Kiểm tra xem email hoặc tài khoản đã tồn tại trong cơ sở dữ liệu chưa
                var existingUser = await _context.KhachHang
                    .FirstOrDefaultAsync(k => k.email == khachHang.email || k.taiKhoan == khachHang.taiKhoan);

                if (existingUser != null)
                {
                    if (existingUser.email == khachHang.email)
                    {
                        ModelState.AddModelError("Email", "Email đã được sử dụng.");
                    }
                    if (existingUser.taiKhoan == khachHang.taiKhoan)
                    {
                        ModelState.AddModelError("TaiKhoan", "Tên tài khoản đã tồn tại.");
                    }
                    return View(khachHang);
                }

                _context.Add(khachHang);
                await _context.SaveChangesAsync();

                // Chuyển hướng đến trang hiển thị thông báo đăng ký thành công
                return RedirectToAction(nameof(RegisterSuccess));
            }

            return View(khachHang);
        }

        // GET: /Account/RegisterSuccess
        [HttpGet]
        public IActionResult RegisterSuccess()
        {
            return View();
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string taiKhoan, string matKhau, bool RememberMe)
        {
            if (ModelState.IsValid)
            {
                // Tìm tài khoản trong cơ sở dữ liệu
                var user = await _context.KhachHang
                    .FirstOrDefaultAsync(k => k.taiKhoan == taiKhoan && k.matKhau == matKhau);

                if (user != null)
                {
                    // Đăng nhập thành công
                    HttpContext.Session.SetString("KhachHangId", user.id.ToString());
                    if (RememberMe)
                    {
                        // Thiết lập cookie RememberMe
                        Response.Cookies.Append("KhachHangId", user.id.ToString(), new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(30) });
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Tài khoản hoặc mật khẩu không đúng.");
                }
            }

            return View();
        }
        // Other methods...

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
