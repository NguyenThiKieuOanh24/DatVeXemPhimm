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
    public class VesController : Controller
    {
        private readonly DatVeXemPhimContext _context;

        public VesController(DatVeXemPhimContext context)
        {
            _context = context;
        }

        // GET: Ves
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ve.ToListAsync());
        }

        // GET: Ves/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ve = await _context.Ve
                .FirstOrDefaultAsync(m => m.id == id);
            if (ve == null)
            {
                return NotFound();
            }

            return View(ve);
        }

        // GET: Ves/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ves/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("iD,maXuatChieu,maKhachHang,maNhanVien,maGhe,ngayBanVe,tongTien")] Ve ve)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ve);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ve);
        }

        // GET: Ves/Edit/5
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
            return View(ve);
        }

        // POST: Ves/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("iD,maXuatChieu,maKhachHang,maNhanVien,maGhe,ngayBanVe,tongTien")] Ve ve)
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
            return View(ve);
        }

        // GET: Ves/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ve = await _context.Ve
                .FirstOrDefaultAsync(m => m.id == id);
            if (ve == null)
            {
                return NotFound();
            }

            return View(ve);
        }

        // POST: Ves/Delete/5
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
        public IActionResult ThoiGian()
        {
            return View();
        }
        public IActionResult DatGhe()
        {
            return View();
        }

        public IActionResult DatVe()
        {
            return View();
        }
    }
}
