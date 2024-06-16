using Microsoft.AspNetCore.Mvc;
using DatVeXemPhim.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using DatVeXemPhim.Data;
using Microsoft.AspNetCore.Authentication;
using DatVeXemPhim.App_Start;
using Microsoft.AspNetCore.Authentication.Cookies;

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
                    /*HttpContext.Session.SetString("KhachHangId", user.id.ToString())*/;
                    SessionConfig.SaveKhachHang(user);
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

       
        public async Task<IActionResult> LogOut()
        {
            SessionConfig.LogOutKhachHang();
            return RedirectToAction("Index", "Home");
        }
        // GET: /Account/Profile
        [HttpGet]
        public async Task<IActionResult> Profile()
        { 
            var user = SessionConfig.GetKhachHang();

            if (user == null)
            {
                return Unauthorized();
            }
            int userId = user.id;
            var khachhang = await _context.KhachHang
                .FirstOrDefaultAsync(v => v.id == userId);
            if(khachhang == null)
            {
                return NotFound();
            }
            await _context.SaveChangesAsync();
            return View(khachhang);
        }
        // GET: /Account/EditProfile
[HttpGet]
public async Task<IActionResult> EditProfile()
{
    var user = SessionConfig.GetKhachHang();

    if (user == null)
    {
        return Unauthorized();
    }

    int userId = user.id;
    var khachHang = await _context.KhachHang.FindAsync(userId);

    if (khachHang == null)
    {
        return NotFound();
    }

    return View(khachHang);
}

        // POST: /Account/EditProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile([Bind("id,hoTen,soDienThoai,email,taiKhoan,matKhau")] KhachHang khachHang)
        {
            if (!ModelState.IsValid)
            {
                return View(khachHang);
            }

            try
            {
                _context.Update(khachHang);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KhachHangExists(khachHang.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Profile));
        }


        private bool KhachHangExists(int id)
{
    return _context.KhachHang.Any(e => e.id == id);
}

        // GET: /Account/ChangePassword
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        // POST: /Account/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            var user = SessionConfig.GetKhachHang();

            if (user == null)
            {
                return Unauthorized();
            }
            int userId = user.id;
            var khachhang = await _context.KhachHang
                .FirstOrDefaultAsync(v => v.id == userId);

            if (khachhang == null)
            {
                return RedirectToAction("Login");
            }

            if (khachhang.matKhau != currentPassword)
            {
                ModelState.AddModelError("CurrentPassword", "Mật khẩu hiện tại không đúng.");
                return View();
            }

            if (newPassword != confirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Mật khẩu xác nhận không khớp.");
                return View();
            }

            khachhang.matKhau = newPassword;
            _context.Update(khachhang);
            await _context.SaveChangesAsync();

            return RedirectToAction("Profile");
        }
    }
}
