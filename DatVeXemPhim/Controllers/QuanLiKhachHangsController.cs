using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DatVeXemPhim.Data;
using DatVeXemPhim.Models;

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
            ViewData["NumberSortParm"] = sortOrder == "Number" ? "TaiKhoan" : "Number";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var khachHangs = from s in _context.KhachHang
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                khachHangs = khachHangs.Where(s => s.hoTen.Contains(searchString)
                                       || s.taiKhoan.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    khachHangs = khachHangs.OrderByDescending(s => s.hoTen);
                    break;
                case "Number":
                    khachHangs = khachHangs.OrderBy(s => s.soDienThoai);
                    break;
                case "TaiKhoan":
                    khachHangs = khachHangs.OrderByDescending(s => s.taiKhoan);
                    break;
                default:
                    khachHangs = khachHangs.OrderBy(s => s.hoTen);
                    break;
            }
            int pageSize = 10;
            return View(await phanTrang<KhachHang>.CreateAsync(khachHangs.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
      

        // GET: QuanLiKhachHangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHang
                .Include(s => s.Ves)
                    .ThenInclude(e => e.fk_XuatChieu)
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,hoTen,soDienThoai,email,taiKhoan,matKhau")] KhachHang khachHang)
        {
            try
            {
                if (ModelState.IsValid)
                {
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


    }
}
