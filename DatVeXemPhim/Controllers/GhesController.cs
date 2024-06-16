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
    public class GhesController : Controller
    {
        private readonly DatVeXemPhimContext _context;

        public GhesController(DatVeXemPhimContext context)
        {
            _context = context;
        }

        // GET: QuanLyGhes
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["NumberSortParm"] = sortOrder == "Number" ? "" : "Number";
            ViewData["CurrentFilter"] = searchString;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            var ghe = from s in _context.Ghe
                             select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                ghe = ghe.Where(s => s.tenGhe.Contains(searchString));
            }

            ghe = sortOrder switch
            {
                "name_desc" => ghe.OrderByDescending(s => s.tenGhe),
                "Number" => ghe.OrderBy(s => s.tenGhe),
                _ => ghe.OrderBy(s => s.tenGhe),
            };

            int pageSize = 5;
            return View(await phanTrang<Ghe>.CreateAsync(ghe.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: QuanLyGhes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ghe = await _context.Ghe
                .FirstOrDefaultAsync(m => m.id == id);
            if (ghe == null)
            {
                return NotFound();
            }

            return View(ghe);
        }

        // GET: QuanLyGhes/Create
        public IActionResult Create()
        {
            ViewData["PhongChieuList"] = new SelectList(_context.PhongChieu, "id", "tenPhong");
            return View();
        }

        // POST: QuanLyGhes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id, maPhong, tenGhe")] Ghe ghe)
        {
            if (ModelState.IsValid)
            {
                bool gheExists = await _context.Ghe.AnyAsync(g => g.tenGhe == ghe.tenGhe && g.maPhong == ghe.maPhong);
                if (gheExists)
                {
                    // Thêm lỗi vào ModelState
                    ModelState.AddModelError("tenGhe", "Tên ghế đã tồn tại trong phòng chiếu này.");
                    return View(ghe);
                }
                _context.Add(ghe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PhongChieuList"] = new SelectList(_context.PhongChieu, "id", "tenPhong", ghe.maPhong);
            return View(ghe);
        }

        // GET: QuanLyGhes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ghe = await _context.Ghe.FindAsync(id);
            if (ghe == null)
            {
                return NotFound();
            }
            ViewData["PhongChieuList"] = new SelectList(_context.PhongChieu, "id", "tenPhong");
            return View(ghe);
        }

        // POST: QuanLyGhes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, [Bind("id, maPhong, tenGhe")] Ghe ghe)

        {
            if (id != ghe.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ghe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GheExists(ghe.id))
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
            ViewData["PhongChieuList"] = new SelectList(_context.PhongChieu, "id", "tenPhong", ghe.maPhong);
            return View(ghe);
        }

        // GET: QuanLyGhes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ghe = await _context.Ghe
                .FirstOrDefaultAsync(m => m.id == id);
            if (ghe == null)
            {
                return NotFound();
            }

            return View(ghe);
        }

        // POST: QuanLyGhes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ghe = await _context.Ghe.FindAsync(id);
            if (ghe != null)
            {
                _context.Ghe.Remove(ghe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GheExists(int id)
        {
            return _context.Ghe.Any(e => e.id == id);
        }
    }
}
