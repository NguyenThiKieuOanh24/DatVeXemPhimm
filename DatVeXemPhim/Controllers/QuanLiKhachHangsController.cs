using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
using ClosedXML.Excel;
using System.Linq;


namespace DatVeXemPhim.Controllers
{
    public class QuanLiKhachHangsController : Controller
    {
        private readonly DatVeXemPhimContext _context;

        public QuanLiKhachHangsController(DatVeXemPhimContext context)
        {
            _context = context;
        }

        // GET: QuanLiKhachHangs
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CurrentFilter"] = searchString;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            var khachHangs = from s in _context.KhachHang
                             select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                khachHangs = khachHangs.Where(s => s.hoTen.Contains(searchString)
                                                || s.taiKhoan.Contains(searchString));
            }

            khachHangs = sortOrder switch
            {
                "name_desc" => khachHangs.OrderByDescending(s => s.hoTen),
                _ => khachHangs.OrderBy(s => s.hoTen),
            };

            int pageSize = 5;
            return View(await phanTrang<KhachHang>.CreateAsync(khachHangs.AsNoTracking(), pageNumber ?? 1, pageSize));
        }



        // GET: QuanLiKhachHangs/Details/5
        //Include(kh => kh.Ves): Tải các vé liên quan đến khách hàng.
        //ThenInclude(v => v.fk_XuatChieu) : Tải các thông tin của xuất chiếu liên quan đến vé.
        //ThenInclude(xc => xc.fk_Phim): Tải thông tin của phim liên quan đến xuất chiếu.
        //Include(kh => kh.Ves): (Lần thứ hai) Tải lại các vé liên quan để tiếp tục nạp các thuộc tính khác của vé.
        //ThenInclude(v => v.fk_MaGhe): Tải thông tin ghế liên quan đến vé.
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHang
                .Include(kh => kh.Ves)
                .ThenInclude(v => v.fk_XuatChieu)
                .ThenInclude(xc => xc.fk_Phim)
                .Include(kh => kh.Ves)
                .ThenInclude(v => v.fk_MaGhe)
                .Include(kh => kh.Ves)
                .ThenInclude(v => v.fk_XuatChieu)
                .ThenInclude(pc => pc.fk_PhongChieu)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.id == id);

            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }


        // GET: QuanLiKhachHangs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: QuanLiKhachHangs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,hoTen,soDienThoai,email,taiKhoan,matKhau")] KhachHang khachHang)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (TaiKhoanDaTonTai(khachHang.taiKhoan))
                    {
                        ModelState.AddModelError(nameof(khachHang.taiKhoan), "Tài khoản đã tồn tại.");
                        return View(khachHang); // Trả về view với thông báo lỗi
                    }

                    _context.Add(khachHang);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(khachHang);
        }

        private bool TaiKhoanDaTonTai(string taiKhoan)
        {
            return _context.KhachHang.Any(kh => kh.taiKhoan == taiKhoan);
        }




        // GET: QuanLiKhachHangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHang.FindAsync(id);
            if (khachHang == null)
            {
                return NotFound();
            }
            return View(khachHang);
        }

        // POST: QuanLiKhachHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,hoTen,soDienThoai,email,taiKhoan,matKhau")] KhachHang khachHang)
        {
            if (id != khachHang.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
                return RedirectToAction(nameof(Index));
            }
            return View(khachHang);
        }

        // GET: QuanLiKhachHangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHang
                .FirstOrDefaultAsync(m => m.id == id);
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }

        // POST: QuanLiKhachHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var khachHang = await _context.KhachHang.FindAsync(id);
            if (khachHang != null)
            {
                _context.KhachHang.Remove(khachHang);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KhachHangExists(int id)
        {
            return _context.KhachHang.Any(e => e.id == id);
        }

        public async Task<IActionResult> LichSuDonHang(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veList = await _context.Ve
                .Include(v => v.fk_XuatChieu)
                    .ThenInclude(h => h.fk_Phim)
                .Include(v => v.fk_XuatChieu)
                    .ThenInclude(x => x.fk_PhongChieu)
                .Include(n => n.fk_MaGhe)
                .AsNoTracking()
                .Where(m => m.id == id)
                .ToListAsync();

            if (veList == null || !veList.Any())
            {
                return NotFound();
            }

            return View(veList);
        }

        [Route("QuanLiKhachHangs/ExportToExcel")]
        public async Task<IActionResult> ExportToExcel()
        {
            // Lấy danh sách khách hàng từ database
            var khachHangs = await _context.KhachHang.ToListAsync();

            // Tạo file Excel
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("KhachHangs");

            // Thêm tiêu đề cột
            var columns = typeof(KhachHang).GetProperties().Select(p => p.Name).ToList();
            for (int i = 0; i < columns.Count; i++)
            {
                worksheet.Cell(1, i + 1).Value = columns[i];
            }

            // Thêm dữ liệu vào worksheet từ dòng thứ 2
            for (int i = 0; i < khachHangs.Count; i++)
            {
                var khachHang = khachHangs[i];
                for (int j = 0; j < columns.Count; j++)
                {
                    var value = typeof(KhachHang).GetProperty(columns[j]).GetValue(khachHang);
                    worksheet.Cell(i + 2, j + 1).Value = value != null ? value.ToString() : ""; // Chuyển đổi giá trị sang chuỗi
                }
            }

            // Lưu workbook vào memory stream
            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            // Trả về file Excel
            string excelName = $"KhachHangs_{DateTime.Now.ToString("HH-mm-ss dd-MM-yyyy")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
        
    }
}
