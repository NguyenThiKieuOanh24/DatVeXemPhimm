using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DatVeXemPhim.Models;

namespace DatVeXemPhim.Data
{
    public class DatVeXemPhimContext : DbContext
    {
        public DatVeXemPhimContext (DbContextOptions<DatVeXemPhimContext> options)
            : base(options)
        {
        }

        public DbSet<DatVeXemPhim.Models.KhachHang> KhachHang { get; set; }
        public DbSet<DatVeXemPhim.Models.Phim> Phim { get; set; }
        public DbSet<DatVeXemPhim.Models.NhanVien> NhanVien { get; set; }
        public DbSet<DatVeXemPhim.Models.Ve> Ve { get; set; }
        public DbSet<DatVeXemPhim.Models.XuatChieu> XuatChieu { get; set; } 
        public DbSet<DatVeXemPhim.Models.Ghe> Ghe { get; set; } 
        public DbSet<DatVeXemPhim.Models.PhongChieu> PhongChieu { get; set; }
        public DbSet<DatVeXemPhim.Models.TheLoaiPhim> TheLoaiPhim { get; set; } 

    }
}
