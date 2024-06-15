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

namespace DatVeXemPhim.Controllers
{
    public class QuanLiXuatChieusController : Controller
    {
        private readonly DatVeXemPhimContext _context;

        public QuanLiXuatChieusController(DatVeXemPhimContext context)
        {
            _context = context;
        }

        // GET: QuanLiXuatChieus
        public async Task<IActionResult> Index(string sortOrder, string searchString, string searchDay, string searchMonth, string searchYear, string currentFilter, int? pageNumber, DateTime? startDate, DateTime? endDate)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentDay"] = searchDay;
            ViewData["CurrentMonth"] = searchMonth;
            ViewData["CurrentYear"] = searchYear;

            if (searchString != null || searchDay != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var xuatChieus = from s in _context.XuatChieu.Include(x => x.fk_Phim).Include(x => x.fk_PhongChieu)
                             select s;

            if (!String.IsNullOrEmpty(searchDay) && int.TryParse(searchDay, out int day))
            {
                xuatChieus = xuatChieus.Where(s => s.ngayChieu.Day == day);
            }
            if (!String.IsNullOrEmpty(searchMonth) && int.TryParse(searchMonth, out int month))
            {
                xuatChieus = xuatChieus.Where(s => s.ngayChieu.Month == month);
            }
            if (!String.IsNullOrEmpty(searchYear) && int.TryParse(searchYear, out int year))
            {
                xuatChieus = xuatChieus.Where(s => s.ngayChieu.Year == year);
            }

            if (startDate.HasValue)
            {
                xuatChieus = xuatChieus.Where(x => x.ngayChieu >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                xuatChieus = xuatChieus.Where(x => x.ngayChieu <= endDate.Value);
            }

            // Truyền giá trị ngày bắt đầu và ngày kết thúc đến view
            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

            switch (sortOrder)
            {
                case "Date":
                    xuatChieus = xuatChieus.OrderBy(s => s.ngayChieu);
                    break;
                case "date_desc":
                    xuatChieus = xuatChieus.OrderByDescending(s => s.ngayChieu);
                    break;
                default:
                    xuatChieus = xuatChieus.OrderBy(s => s.ngayChieu);
                    break;
            }

            int pageSize = 5;
            return View(await phanTrang<XuatChieu>.CreateAsync(xuatChieus.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
       

        // GET: QuanLiXuatChieus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xuatChieu = await _context.XuatChieu
                .Include(x => x.fk_Phim)
                .Include(x => x.fk_PhongChieu)
                .FirstOrDefaultAsync(m => m.id == id);
            if (xuatChieu == null)
            {
                return NotFound();
            }

            return View(xuatChieu);
        }

        // GET: QuanLiXuatChieus/Create
        public IActionResult Create()
        {
            ViewData["maPhim"] = new SelectList(_context.Phim, "id", "tenPhim");
            ViewData["maPhong"] = new SelectList(_context.PhongChieu, "id", "tenPhong");
            return View();
        }

        // POST: QuanLiXuatChieus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,maPhong,maPhim,ngayChieu,gioBatDau,gioKetThuc")] XuatChieu xuatChieu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(xuatChieu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["maPhim"] = new SelectList(_context.Phim, "id", "id", xuatChieu.maPhim);
            ViewData["maPhong"] = new SelectList(_context.PhongChieu, "id", "id", xuatChieu.maPhong);
            return View(xuatChieu);
        }

        // GET: QuanLiXuatChieus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xuatChieu = await _context.XuatChieu.FindAsync(id);
            if (xuatChieu == null)
            {
                return NotFound();
            }
            ViewData["maPhim"] = new SelectList(_context.Phim, "id", "tenPhim", xuatChieu.maPhim);
            ViewData["maPhong"] = new SelectList(_context.PhongChieu, "id", "tenPhong", xuatChieu.maPhong);
            return View(xuatChieu);
        }

        // POST: QuanLiXuatChieus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,maPhong,maPhim,ngayChieu,gioBatDau,gioKetThuc")] XuatChieu xuatChieu)
        {
            if (id != xuatChieu.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(xuatChieu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!XuatChieuExists(xuatChieu.id))
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
            ViewData["maPhim"] = new SelectList(_context.Phim, "id", "id", xuatChieu.maPhim);
            ViewData["maPhong"] = new SelectList(_context.PhongChieu, "id", "id", xuatChieu.maPhong);
            return View(xuatChieu);
        }

        // GET: QuanLiXuatChieus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xuatChieu = await _context.XuatChieu
                .Include(x => x.fk_Phim)
                .Include(x => x.fk_PhongChieu)
                .FirstOrDefaultAsync(m => m.id == id);
            if (xuatChieu == null)
            {
                return NotFound();
            }

            return View(xuatChieu);
        }

        // POST: QuanLiXuatChieus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var xuatChieu = await _context.XuatChieu.FindAsync(id);
            if (xuatChieu != null)
            {
                _context.XuatChieu.Remove(xuatChieu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool XuatChieuExists(int id)
        {
            return _context.XuatChieu.Any(e => e.id == id);
        }

        [Route("QuanLiXuatChieus/ExportToExcel")]
        public async Task<IActionResult> ExportToExcel()
        {
            // Lấy danh sách xuất chiếu từ database
            var xuatChieus = await _context.XuatChieu.ToListAsync();

            // Tạo file Excel
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("XuatChieus");

            // Thêm tiêu đề cột
            var columns = typeof(XuatChieu).GetProperties().Select(p => p.Name).ToList();
            for (int i = 0; i < columns.Count; i++)
            {
                worksheet.Cell(1, i + 1).Value = columns[i];
            }

            // Thêm dữ liệu vào worksheet từ dòng thứ 2
            for (int i = 0; i < xuatChieus.Count; i++)
            {
                var xuatChieu = xuatChieus[i];
                for (int j = 0; j < columns.Count; j++)
                {
                    var value = typeof(XuatChieu).GetProperty(columns[j]).GetValue(xuatChieu);
                    worksheet.Cell(i + 2, j + 1).Value = value != null ? value.ToString() : ""; // Chuyển đổi giá trị sang chuỗi
                }
            }

            // Lưu workbook vào memory stream
            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            // Trả về file Excel
            string excelName = $"XuatChieus_{DateTime.Now.ToString("HH-mm-ss dd-MM-yyyy")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}
