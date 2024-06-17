using DatVeXemPhim.App_Start;
using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim.Controllers
{
    public class KhachDatVeController : Controller
    {
        private readonly DatVeXemPhimContext _context;

        public KhachDatVeController(DatVeXemPhimContext context)
        {
            _context = context;
        }

        // GET: Step2
        public async Task<IActionResult> Step2(int? id)
        {
            // Kiểm tra xem có người dùng nào đã đăng nhập hay không
            var khachHang = SessionConfig.GetKhachHang();

            if (khachHang == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var currentDateTime = DateTime.Now;

            var hasUpcomingShowtime = _context.XuatChieu
                .Where(x => x.maPhim == id && x.ngayChieu >= currentDateTime)
                .ToList();

            if (!hasUpcomingShowtime.Any())
            {
                return RedirectToAction("Index", "Home");
            }


            var xuatChieus = _context.XuatChieu.ToList();

            // Lấy danh sách các ghế đã được chọn


            ViewBag.UpcomingShowtimes = new SelectList(hasUpcomingShowtime,"id", "ngayChieu");

            var selectedSeats = _context.Ve.Select(v => v.maGhe).ToList();

            var availableSeats = _context.Ghe.Where(g => !selectedSeats.Contains(g.id)).ToList();

            ViewBag.Ghes = new SelectList(availableSeats, "id", "tenGhe");

            return View();
        }

        // POST: Step2
        [HttpPost]
        public IActionResult Step2(Ve ve)
        {
            if (ModelState.IsValid)
            {
                // Lấy thông tin khách hàng từ database
                var khachHang = SessionConfig.GetKhachHang().id;
                var xuatChieu = _context.XuatChieu.FirstOrDefault(x => x.id == ve.maXuatChieu);
                var ghe = _context.Ghe.FirstOrDefault(g => g.id == ve.maGhe);
                // Lấy thông tin nhân viên có id = 1 từ database
                var nhanVien = 1;

                Ve ves = new Ve
                {
                    maXuatChieu = xuatChieu.id,
                    maKhachHang = khachHang,
                    maGhe = ghe.id,
                    maNhanVien = nhanVien,
                    ngayBanVe = DateTime.Now, // Cập nhật ngày bán vé
                    tongTien = 45000 // Giá vé mặc định
                };

                _context.Ve.Add(ves);

                _context.SaveChanges();

                return RedirectToAction("VeDaDat", "Home");
            }
                
        // Nếu ModelState không hợp lệ, lấy lại danh sách để hiển th

            return View(ve);
        }

        // GET: Confirmation
        public IActionResult Confirmation(int id)
        {
            // Lấy thông tin của vé từ database dựa trên id đã được truyền từ action Step2
            var ve = _context.Ve
                .Include(v => v.fk_XuatChieu)
                .ThenInclude(x => x.fk_Phim)
                .Include(v => v.fk_MaGhe)
                .FirstOrDefault(v => v.id == id);

            if (ve == null)
            {
                return NotFound(); // Trường hợp không tìm thấy vé
            }

            return View(ve); // Trả về view Confirmation với dữ liệu của vé
        }
    }
}
