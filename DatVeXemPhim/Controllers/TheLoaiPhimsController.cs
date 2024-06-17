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
    public class TheLoaiPhimsController : Controller
    {
        private readonly DatVeXemPhimContext _context;

        public TheLoaiPhimsController(DatVeXemPhimContext context)
        {
            _context = context;
        }

        // GET: QuanLyGhes
        public async Task<IActionResult> Index(string searchString, string currentFilter, int? pageNumber)
        {
            ViewData["CurrentFilter"] = searchString;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            var theLoaiPhim = from s in _context.TheLoaiPhim
                             select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                theLoaiPhim = theLoaiPhim.Where(s => s.tenLoaiPhim.Contains(searchString));
            }

            int pageSize = 5;
            return View(await phanTrang<TheLoaiPhim>.CreateAsync(theLoaiPhim.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: QuanLyGhes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theLoaiPhim = await _context.TheLoaiPhim
                .FirstOrDefaultAsync(m => m.id == id);
            if (theLoaiPhim == null)
            {
                return NotFound();
            }

            return View(theLoaiPhim);
        }

        // GET: QuanLyGhes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: QuanLyGhes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id, tenLoaiPhim")] TheLoaiPhim theLoaiPhim)
        {
            if (ModelState.IsValid)
            {
                bool theLoaiPhimExists = await _context.TheLoaiPhim.AnyAsync(p => p.tenLoaiPhim == theLoaiPhim.tenLoaiPhim);
                if (theLoaiPhimExists)
                {
                    // Thêm lỗi vào ModelState
                    ModelState.AddModelError("tenLoaiPhim", "Tên thể loại đã tồn tại.");
                    return View(theLoaiPhim);
                }
                _context.Add(theLoaiPhim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(theLoaiPhim);
        }

        // GET: QuanLyGhes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theLoaiPhim = await _context.TheLoaiPhim.FindAsync(id);
            if (theLoaiPhim == null)
            {
                return NotFound();
            }
            return View(theLoaiPhim);
        }

        // POST: QuanLyGhes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id, tenLoaiPhim")] TheLoaiPhim theLoaiPhim)

        {
            if (id != theLoaiPhim.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(theLoaiPhim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TheLoaiPhimExists(theLoaiPhim.id))
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
            return View(theLoaiPhim);
        }

        // GET: QuanLyGhes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theLoaiPhim = await _context.TheLoaiPhim
                .FirstOrDefaultAsync(m => m.id == id);
            if (theLoaiPhim == null)
            {
                return NotFound();
            }

            return View(theLoaiPhim);
        }

        // POST: QuanLyGhes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var theLoaiPhim = await _context.TheLoaiPhim.FindAsync(id);
            if (theLoaiPhim != null)
            {
                _context.TheLoaiPhim.Remove(theLoaiPhim);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TheLoaiPhimExists(int id)
        {
            return _context.TheLoaiPhim.Any(e => e.id == id);
        }
    }
}
