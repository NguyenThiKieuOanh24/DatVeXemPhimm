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
        public async Task<IActionResult> Index()
        {
            return View(await _context.TheLoaiPhim.ToListAsync());
        }

        // GET: QuanLyGhes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theLoaiPhim = await _context.TheLoaiPhim
<<<<<<< HEAD
                .FirstOrDefaultAsync(m => m.id == id);
=======
                .FirstOrDefaultAsync(m => m.iD == id);
>>>>>>> 6d85becf45debd6a7bb0ef55dd8d8fba8adfbd46
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

<<<<<<< HEAD
        public async Task<IActionResult> Create([Bind("id, tenLoaiPhim")] TheLoaiPhim theLoaiPhim)
=======
        public async Task<IActionResult> Create([Bind("iD, tenLoaiPhim")] TheLoaiPhim theLoaiPhim)
>>>>>>> 6d85becf45debd6a7bb0ef55dd8d8fba8adfbd46

        {
            if (ModelState.IsValid)
            {
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



<<<<<<< HEAD
        public async Task<IActionResult> Edit(int id, [Bind("id, tenLoaiPhim")] TheLoaiPhim theLoaiPhim)

        {
            if (id != theLoaiPhim.id)
=======
        public async Task<IActionResult> Edit(int id, [Bind("iD, tenLoaiPhim")] TheLoaiPhim theLoaiPhim)

        {
            if (id != theLoaiPhim.iD)
>>>>>>> 6d85becf45debd6a7bb0ef55dd8d8fba8adfbd46
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
<<<<<<< HEAD
                    if (!TheLoaiPhimExists(theLoaiPhim.id))
=======
                    if (!TheLoaiPhimExists(theLoaiPhim.iD))
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
<<<<<<< HEAD
                .FirstOrDefaultAsync(m => m.id == id);
=======
                .FirstOrDefaultAsync(m => m.iD == id);
>>>>>>> 6d85becf45debd6a7bb0ef55dd8d8fba8adfbd46
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
<<<<<<< HEAD
            return _context.TheLoaiPhim.Any(e => e.id == id);
=======
            return _context.TheLoaiPhim.Any(e => e.iD == id);
>>>>>>> 6d85becf45debd6a7bb0ef55dd8d8fba8adfbd46
        }
    }
}
