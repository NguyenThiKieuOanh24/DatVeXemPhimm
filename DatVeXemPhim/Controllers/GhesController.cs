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
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ghe.ToListAsync());
        }

        // GET: QuanLyGhes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ghe = await _context.Ghe
<<<<<<< HEAD
                .FirstOrDefaultAsync(m => m.id == id);
=======
                .FirstOrDefaultAsync(m => m.iD == id);
>>>>>>> 6d85becf45debd6a7bb0ef55dd8d8fba8adfbd46
            if (ghe == null)
            {
                return NotFound();
            }

            return View(ghe);
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

<<<<<<< HEAD
        public async Task<IActionResult> Create([Bind("id, tenGhe")] Ghe ghe)
=======
        public async Task<IActionResult> Create([Bind("iD, maPhong, tenGhe")] Ghe ghe)
>>>>>>> 6d85becf45debd6a7bb0ef55dd8d8fba8adfbd46

        {
            if (ModelState.IsValid)
            {
                _context.Add(ghe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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
            return View(ghe);
        }

        // POST: QuanLyGhes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]



<<<<<<< HEAD
        public async Task<IActionResult> Edit(int id, [Bind("id, tenGhe")] Ghe ghe)

        {
            if (id != ghe.id)
=======
        public async Task<IActionResult> Edit(int id, [Bind("iD, maPhong, tenghe")] Ghe ghe)

        {
            if (id != ghe.iD)
>>>>>>> 6d85becf45debd6a7bb0ef55dd8d8fba8adfbd46
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
<<<<<<< HEAD
                    if (!GheExists(ghe.id))
=======
                    if (!GheExists(ghe.iD))
>>>>>>> 6d85becf45debd6a7bb0ef55dd8d8fba8adfbd46
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
<<<<<<< HEAD
                .FirstOrDefaultAsync(m => m.id == id);
=======
                .FirstOrDefaultAsync(m => m.iD == id);
>>>>>>> 6d85becf45debd6a7bb0ef55dd8d8fba8adfbd46
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
<<<<<<< HEAD
            return _context.Ghe.Any(e => e.id == id);
=======
            return _context.Ghe.Any(e => e.iD == id);
>>>>>>> 6d85becf45debd6a7bb0ef55dd8d8fba8adfbd46
        }
    }
}
