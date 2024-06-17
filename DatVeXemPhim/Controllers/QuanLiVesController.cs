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
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber, string searchCodeXC, string searchCodeKH, string searchCodeV)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentCodeV"] = searchCodeV;
            ViewData["CurrentCodeXC"] = searchCodeXC;
            ViewData["CurrentCodeKH"] = searchCodeKH;

            // Lưu lại filter hiện tại để giữ trạng thái khi chuyển trang
            if (searchString != null || searchCodeXC != null || searchCodeKH != null || searchCodeV != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            // Bắt đầu truy vấn
            var ves = from s in _context.Ve
                      .Include(v => v.fk_KhachHang)
                      .Include(v => v.fk_MaGhe)
                      .Include(v => v.fk_NhanVien)
                      .Include(v => v.fk_XuatChieu)
                      .ThenInclude(x => x.fk_PhongChieu)
                      .Include(v => v.fk_XuatChieu)
                      .ThenInclude(p => p.fk_Phim)
                      select s;

            // Thêm điều kiện tìm kiếm cho mã xuất chiếu
            if (!String.IsNullOrEmpty(searchCodeXC))
            {
                int codeXC;
                if (Int32.TryParse(searchCodeXC, out codeXC))
                {
                    ves = ves.Where(s => s.fk_XuatChieu.id == codeXC);
                }
            }

            // Thêm điều kiện tìm kiếm cho mã khách hàng
            if (!String.IsNullOrEmpty(searchCodeKH))
            {
                int codeKH;
                if (Int32.TryParse(searchCodeKH, out codeKH))
                {
                    ves = ves.Where(s => s.fk_KhachHang.id == codeKH);
                }
            }

            // Thêm điều kiện tìm kiếm cho mã vé
            if (!String.IsNullOrEmpty(searchCodeV))
            {
                int codeV;
                if (Int32.TryParse(searchCodeV, out codeV))
                {
                    ves = ves.Where(s => s.id == codeV);
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
                .ThenInclude(p => p.fk_PhongChieu)
                .Include(v => v.fk_XuatChieu)
                .ThenInclude(p => p.fk_Phim)
                .FirstOrDefaultAsync(m => m.id == id);
            if (ve == null)
            {
                return NotFound();
            }

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
                .ThenInclude(p => p.fk_PhongChieu)
                .Include(v => v.fk_XuatChieu)
                .ThenInclude(p => p.fk_Phim)
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
    }
}
