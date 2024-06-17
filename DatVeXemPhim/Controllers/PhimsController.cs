using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DatVeXemPhim.Data;
using DatVeXemPhim.Models;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;

namespace DatVeXemPhim.Controllers
{
    public class PhimsController : Controller
    {
        private readonly DatVeXemPhimContext _context; //Khai báo một đối tượng context của Entity Framework để truy cập cơ sở dữ liệu.

        public PhimsController(DatVeXemPhimContext context) //Constructor nhận một đối tượng context và gán nó cho biến _context.
        {
            _context = context;
        }

        // GET: Phims
        public async Task<IActionResult> Index(string searchString, string currentFilter, int? pageNumber)//Phương thức này xử lý yêu cầu GET để hiển thị danh sách các phim với khả năng tìm kiếm và phân trang.
        {
            ViewData["CurrentFilter"] = searchString; // Lưu trữ giá trị của searchString để sử dụng trong view.

            if (searchString != null)//Nếu có giá trị tìm kiếm mới, đặt pageNumber về 1
            {
                pageNumber = 1;
            }
            else //ngược lại sử dụng giá trị hiện tại.
            {
                searchString = currentFilter;
            }


            var phim = from s in _context.Phim.Include(x => x.fk_TheLoaiPhim)
                      select s;//Truy vấn tất cả các phim từ cơ sở dữ liệu.

            if (!String.IsNullOrEmpty(searchString))//Nếu có giá trị tìm kiếm, lọc các phim theo tên phim, diễn viên hoặc đạo diễn.
            {
                phim = phim.Where(s => s.tenPhim.Contains(searchString) 
                                       || s.dienVien.Contains(searchString) 
                                       || s.daoDien.Contains(searchString));
            }

            int pageSize = 5;//Đặt kích thước trang là 5.
            return View(await phanTrang<Phim>.CreateAsync(phim.AsNoTracking(), pageNumber ?? 1, pageSize));//Trả về view với danh sách các phim được phân trang.
        }

        // GET: Phims/Details/5
        public async Task<IActionResult> Details(int? id)//Phương thức này xử lý yêu cầu GET để hiển thị chi tiết của một phim theo id.
        {
            if (id == null)//Nếu id là null, trả về kết quả không tìm thấy.
            {
                return NotFound();
            }

            var phim = _context.Phim.Include(p => p.fk_TheLoaiPhim).FirstOrDefault();
            if (phim == null)//Nếu không tìm thấy phim, trả về kết quả không tìm thấy.
            {
                return NotFound ();
            }
            await _context.SaveChangesAsync();
            return View(phim);//Trả về view với thông tin chi tiết của phim.

        }

        // GET: Phims/Create
        public IActionResult Create()//Phương thức này xử lý yêu cầu GET để hiển thị form tạo phim mới.
        {
            ViewData["TheLoaiPhimList"] = new SelectList(_context.TheLoaiPhim, "id", "tenLoaiPhim");//Tạo danh sách các thể loại phim để hiển thị trong dropdown list.
            return View();//Trả về view với form tạo phim.
        }

        // POST: Phims/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]//Đánh dấu phương thức này để xử lý yêu cầu POST.
        [ValidateAntiForgeryToken]//Bảo vệ chống lại các cuộc tấn công CSRF.
        public async Task<IActionResult> Create([Bind("id, posterPhim, tenPhim, daoDien, dienVien, maLoaiPhim, thoiGianKhoiChieu, thoiLuong, ngonNgu")] Phim phim)//Phương thức này xử lý dữ liệu từ form tạo phim.
        {
            if (ModelState.IsValid) //Kiểm tra tính hợp lệ của dữ liệu.
            {
                bool phimExists = await _context.Phim.AnyAsync(p =>//Kiểm tra xem phim đã tồn tại trong cơ sở dữ liệu chưa.
                p.posterPhim == phim.posterPhim &&
                p.tenPhim == phim.tenPhim &&
                p.daoDien == phim.daoDien &&
                p.dienVien == phim.dienVien &&
                p.maLoaiPhim == phim.maLoaiPhim &&
                p.thoiGianKhoiChieu == phim.thoiGianKhoiChieu &&
                p.thoiLuong == phim.thoiLuong &&
                p.ngonNgu == phim.ngonNgu);

                if (phimExists)//Nếu phim đã tồn tại, thêm lỗi vào ModelState và trả về view với thông báo lỗi.
                {
                    ModelState.AddModelError("", "Bộ phim đã tồn tại.");
                    return View(phim);
                }
                _context.Add(phim); //Thêm phim mới vào cơ sở dữ liệu
                await _context.SaveChangesAsync();//lưu thay đổi.
                return RedirectToAction(nameof(Index));//Chuyển hướng về trang danh sách phim sau khi thêm thành công.
            }
            ViewData["TheLoaiPhimList"] = new SelectList(_context.TheLoaiPhim, "id", "tenPhong", phim.maLoaiPhim);
            return View(phim);
        }

        // GET: Phims/Edit/5
        public async Task<IActionResult> Edit(int? id)//Phương thức này xử lý yêu cầu GET để hiển thị form chỉnh sửa phim theo id.
        {
            if (id == null)//Nếu id là null, trả về kết quả không tìm thấy.
            {
                return NotFound();
            }

            var phim = await _context.Phim.FindAsync(id);//Tìm phim theo id.
            if (phim == null)//Nếu không tìm thấy phim, trả về kết quả không tìm thấy.
            {
                return NotFound();
            }
            ViewData["TheLoaiPhimList"] = new SelectList(_context.TheLoaiPhim, "id", "tenLoaiPhim");
            return View(phim);
        }

        // POST: Phims/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id, posterPhim, tenPhim, daoDien, dienVien, maLoaiPhim, thoiGianKhoiChieu, thoiLuong, ngonNgu")] Phim phim)//Phương thức này xử lý dữ liệu từ form chỉnh sửa phim.
        {
            if (id != phim.id)//Nếu id không khớp với id của phim, trả về kết quả không tìm thấy.
            {
                return NotFound();
            }

            if (ModelState.IsValid)// Kiểm tra tính hợp lệ của dữ liệu.
            {
                //Cập nhật phim trong cơ sở dữ liệu và xử lý các lỗi cập nhật đồng thời.
                try
                {
                    _context.Update(phim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhimExists(phim.id))
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
            ViewData["TheLoaiPhimList"] = new SelectList(_context.TheLoaiPhim, "id", "tenPhong", phim.maLoaiPhim);
            return View(phim);
        }

        // GET: Phims/Delete/5
        public async Task<IActionResult> Delete(int? id)//Phương thức này xử lý yêu cầu GET để hiển thị form xác nhận xóa phim theo id.
        {
            if (id == null)//Nếu id là null, trả về kết quả không tìm thấy.
            {
                return NotFound();
            }

            var phim = await _context.Phim.Include(P => P.fk_TheLoaiPhim)
                .FirstOrDefaultAsync(m => m.id == id);
            if (phim == null)
            {
                return NotFound();
            }

            return View(phim);
        }

        // POST: Phims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)//Phương thức này xử lý yêu cầu POST để xác nhận xóa phim theo id.
        {
            var phim = await _context.Phim.FindAsync(id);//Tìm phim theo id.
            if (phim != null)//Nếu tìm thấy phim, xóa nó khỏi cơ sở dữ liệu.
            {
                _context.Phim.Remove(phim);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhimExists(int id)//Phương thức này kiểm tra xem phim có tồn tại trong cơ sở dữ liệu hay không dựa vào id.
        {
            return _context.Phim.Any(e => e.id == id);//Trả về true nếu có phim với id được chỉ định, ngược lại trả về false.
        }
        public async Task<IActionResult> NowShowing()
        {
            var NowShowing = await _context.Phim
                 .Where(p => p.thoiGianKhoiChieu <= DateTime.Now)
                 .ToListAsync();

            return View(NowShowing);
        }
        // GET: Movies/Upcoming
        public async Task<IActionResult> Upcoming()
        {
            var UpComing = await _context.Phim
                .Where(p => p.thoiGianKhoiChieu > DateTime.Now)
                .ToListAsync();

            return View(UpComing);
        }
    }
}