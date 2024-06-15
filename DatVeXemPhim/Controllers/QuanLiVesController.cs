using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
using Microsoft.Data.SqlClient;
using ClosedXML.Excel;



namespace DatVeXemPhim.Controllers
{
    public class QuanLiVesController : Controller
    {
        private readonly DatVeXemPhimContext _context;

        public QuanLiVesController(DatVeXemPhimContext context)
        {
            _context = context;
        }

        // GET: QuanLiVes
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber, string searchCodeXC, string searchCodeKH)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentCodeXC"] = searchCodeXC;
            ViewData["CurrentCodeKH"] = searchCodeKH;

            // Lưu lại filter hiện tại để giữ trạng thái khi chuyển trang
            if (searchString != null || searchCodeXC != null || searchCodeKH != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            // Bắt đầu truy vấn
            var ves = from s in _context.Ve.Include(v => v.fk_KhachHang).Include(v => v.fk_MaGhe).Include(v => v.fk_NhanVien).Include(v => v.fk_XuatChieu)
                      select s;

            // Thêm điều kiện tìm kiếm cho mã xuất chiếu
            if (!String.IsNullOrEmpty(searchCodeXC))
            {
                int codeXC;
                if (Int32.TryParse(searchCodeXC, out codeXC))
                {
                    ves = ves.Where(s => s.fk_XuatChieu.id.ToString().Contains(codeXC.ToString()));
                }
            }

            // Thêm điều kiện tìm kiếm cho mã khách hàng
            if (!String.IsNullOrEmpty(searchCodeKH))
            {
                int codeKH;
                if (Int32.TryParse(searchCodeKH, out codeKH))
                {
                    ves = ves.Where(s => s.fk_KhachHang.id.ToString().Contains(codeKH.ToString()));
                }
            }


            int pageSize = 5;
            return View(await phanTrang<Ve>.CreateAsync(ves.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        // GET: QuanLiVes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ve = await _context.Ve
                .Include(v => v.fk_KhachHang)
                .Include(v => v.fk_MaGhe)
                .Include(v => v.fk_NhanVien)
                .Include(v => v.fk_XuatChieu)
                .FirstOrDefaultAsync(m => m.id == id);
            if (ve == null)
            {
                return NotFound();
            }

            return View(ve);
        }

        // GET: QuanLiVes/Create
        public IActionResult Create()
        {
            ViewData["maKhachHang"] = new SelectList(_context.KhachHang, "id", "id");
            ViewData["maGhe"] = new SelectList(_context.Ghe, "id", "id");
            ViewData["maNhanVien"] = new SelectList(_context.NhanVien, "id", "id");
            ViewData["maXuatChieu"] = new SelectList(_context.XuatChieu, "id", "id");
            return View();
        }

        // POST: QuanLiVes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,maXuatChieu,maKhachHang,maNhanVien,maGhe,ngayBanVe,tongTien")] Ve ve)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ve);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["maKhachHang"] = new SelectList(_context.KhachHang, "id", "id", ve.maKhachHang);
            ViewData["maGhe"] = new SelectList(_context.Ghe, "id", "id", ve.maGhe);
            ViewData["maNhanVien"] = new SelectList(_context.NhanVien, "id", "id", ve.maNhanVien);
            ViewData["maXuatChieu"] = new SelectList(_context.XuatChieu, "id", "id", ve.maXuatChieu);
            return View(ve);
        }

        // GET: QuanLiVes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ve = await _context.Ve.FindAsync(id);
            if (ve == null)
            {
                return NotFound();
            }
            ViewData["maKhachHang"] = new SelectList(_context.KhachHang, "id", "id", ve.maKhachHang);
            ViewData["maGhe"] = new SelectList(_context.Ghe, "id", "id", ve.maGhe);
            ViewData["maNhanVien"] = new SelectList(_context.NhanVien, "id", "id", ve.maNhanVien);
            ViewData["maXuatChieu"] = new SelectList(_context.XuatChieu, "id", "id", ve.maXuatChieu);
            return View(ve);
        }

        // POST: QuanLiVes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,maXuatChieu,maKhachHang,maNhanVien,maGhe,ngayBanVe,tongTien")] Ve ve)
        {
            if (id != ve.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ve);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeExists(ve.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["maKhachHang"] = new SelectList(_context.KhachHang, "id", "id", ve.maKhachHang);
            ViewData["maGhe"] = new SelectList(_context.Ghe, "id", "id", ve.maGhe);
            ViewData["maNhanVien"] = new SelectList(_context.NhanVien, "id", "id", ve.maNhanVien);
            ViewData["maXuatChieu"] = new SelectList(_context.XuatChieu, "id", "id", ve.maXuatChieu);
            return View(ve);
        }

        // GET: QuanLiVes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ve = await _context.Ve
                .Include(v => v.fk_KhachHang)
                .Include(v => v.fk_MaGhe)
                .Include(v => v.fk_NhanVien)
                .Include(v => v.fk_XuatChieu)
                .FirstOrDefaultAsync(m => m.id == id);
            if (ve == null)
            {
                return NotFound();
            }

            return View(ve);
        }

        // POST: QuanLiVes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ve = await _context.Ve.FindAsync(id);
            if (ve != null)
            {
                _context.Ve.Remove(ve);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VeExists(int id)
        {
            return _context.Ve.Any(e => e.id == id);
        }

        [Route("QuanLiVes/ExportToExcel")]
        public async Task<IActionResult> ExportToExcel()
        {
            // Lấy danh sách vé từ database
            var ves = await _context.Ve.ToListAsync();

            // Tạo file Excel
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Ves");

            // Thêm tiêu đề cột
            var columns = typeof(Ve).GetProperties().Select(p => p.Name).ToList();
            for (int i = 0; i < columns.Count; i++)
            {
                worksheet.Cell(1, i + 1).Value = columns[i];
            }

            // Thêm dữ liệu vào worksheet từ dòng thứ 2
            for (int i = 0; i < ves.Count; i++)
            {
                var ve = ves[i];
                for (int j = 0; j < columns.Count; j++)
                {
                    var value = typeof(Ve).GetProperty(columns[j]).GetValue(ve);
                    worksheet.Cell(i + 2, j + 1).Value = value != null ? value.ToString() : ""; // Chuyển đổi giá trị sang chuỗi
                }
            }

            // Lưu workbook vào memory stream
            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            // Trả về file Excel
            string excelName = $"Ves_{DateTime.Now.ToString("HH-mm-ss dd-MM-yyyy")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        public IActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DangKy(KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingKhachHang = _context.KhachHang.FirstOrDefault(kh => kh.taiKhoan == khachHang.taiKhoan);

                    if (existingKhachHang != null)
                    {
                        // Tài khoản đã tồn tại, lưu ID và thông báo
                        TempData["Message"] = "Tài khoản đã tồn tại.";
                        TempData["KhachHangID"] = existingKhachHang.id;
                        return View(khachHang); // Hiển thị lại view để hiển thị thông báo
                    }

                    // Tài khoản chưa tồn tại, thêm mới và chuyển sang DatVe
                    _context.KhachHang.Add(khachHang);
                    _context.SaveChanges();

                    TempData["KhachHangID"] = khachHang.id;
                    return RedirectToAction("DatVe");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi lưu thông tin. Vui lòng thử lại.");
                    // Log lỗi nếu cần thiết
                }
            }

            return View(khachHang);
        }






        public IActionResult DatVe()
        {
            var khachHangID = TempData["KhachHangID"];
            if (khachHangID == null)
            {
                return RedirectToAction("DangKy");
            }

            var phims = _context.Phim.ToList();
            var xuatChieus = _context.XuatChieu.ToList();
            var ghes = _context.Ghe.ToList();
            var nhanViens = _context.NhanVien.ToList();

            ViewBag.Phims = new SelectList(phims, "id", "tenPhim");
            ViewBag.XuatChieus = new SelectList(xuatChieus, "id", "ngayChieu");
            ViewBag.Ghes = new SelectList(ghes, "id", "tenGhe");
            ViewBag.NhanViens = new SelectList(nhanViens, "id", "hoTen");
            ViewBag.KhachHangID = khachHangID;

            return View();
        }


        // POST: DatVe
        [HttpPost]
        public IActionResult DatVe(Ve ve)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Lấy thông tin khách hàng từ database
                    var khachHang = _context.KhachHang.FirstOrDefault(k => k.id == ve.maKhachHang);
                    var xuatChieu = _context.XuatChieu.FirstOrDefault(x => x.id == ve.maXuatChieu);
                    var ghe = _context.Ghe.FirstOrDefault(g => g.id == ve.maGhe);
                    var nhanVien = _context.NhanVien.FirstOrDefault(nv => nv.id == ve.maNhanVien);

                    if (khachHang == null || xuatChieu == null || ghe == null || nhanVien == null)
                    {
                        ModelState.AddModelError("", "Thông tin khách hàng, xuất chiếu hoặc ghế không hợp lệ.");
                        return View(ve);
                    }

                    ve.fk_XuatChieu = xuatChieu;
                    ve.fk_KhachHang = khachHang;
                    ve.fk_MaGhe = ghe;
                    ve.fk_NhanVien = nhanVien;
                    ve.ngayBanVe = DateTime.Now; // Cập nhật ngày bán vé
                    ve.tongTien = 45; // Giá vé mặc định

                    _context.Ve.Add(ve);
                    _context.SaveChanges();

                    return RedirectToAction("ThanhCong", new { id = ve.id });
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi lưu dữ liệu. Vui lòng thử lại.");
                    // Log lỗi nếu cần thiết
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra. Vui lòng thử lại.");
                    // Log lỗi nếu cần thiết
                }
            }

            // Nếu ModelState không hợp lệ, lấy lại danh sách để hiển thị
            var phims = _context.Phim.ToList();
            var xuatChieus = _context.XuatChieu.ToList();
            var ghes = _context.Ghe.ToList();
            var nhanViens = _context.NhanVien.ToList();

            ViewBag.Phims = new SelectList(phims, "id", "tenPhim");
            ViewBag.XuatChieus = new SelectList(xuatChieus, "id", "ngayChieu");
            ViewBag.Ghes = new SelectList(ghes, "id", "tenGhe");
            ViewBag.NhanViens = new SelectList(nhanViens, "id", "hoTen");

            return View(ve);
        }

        // GET: ThanhCong
        public IActionResult ThanhCong(int id)
        {
            // Lấy thông tin của vé từ database dựa trên id đã được truyền từ action DatVe
            var ve = _context.Ve
                .Include(v => v.fk_XuatChieu)
                .ThenInclude(x => x.fk_Phim)
                .Include(v => v.fk_MaGhe)
                .FirstOrDefault(v => v.id == id);

            if (ve == null)
            {
                return NotFound(); // Trường hợp không tìm thấy vé
            }

            return View(ve); // Trả về view ThanhCong với dữ liệu của vé
        }

    }
}
