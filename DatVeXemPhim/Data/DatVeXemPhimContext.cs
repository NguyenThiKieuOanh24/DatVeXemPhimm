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

        public DbSet<DatVeXemPhim.Models.KhachHang> KhachHang { get; set; } = default!;
        public DbSet<DatVeXemPhim.Models.Phim> Phim { get; set; } = default!;
        public DbSet<DatVeXemPhim.Models.NhanVien> NhanVien { get; set; } = default!;
        public DbSet<DatVeXemPhim.Models.Ve> Ve { get; set; } = default!;
        public DbSet<DatVeXemPhim.Models.XuatChieu> XuatChieu { get; set; } = default!;
        public DbSet<DatVeXemPhim.Models.Ghe> Ghe { get; set; } = default!;
        public DbSet<DatVeXemPhim.Models.PhongChieu> PhongChieu { get; set; } = default!;
        public DbSet<DatVeXemPhim.Models.TheLoaiPhim> TheLoaiPhim { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KhachHang>().ToTable("KhachHang");
            modelBuilder.Entity<Phim>().ToTable("Phim");
            modelBuilder.Entity<NhanVien>().ToTable("NhanVien");
            modelBuilder.Entity<Ve>().ToTable("Ve");
            modelBuilder.Entity<XuatChieu>().ToTable("XuatChieu");
            modelBuilder.Entity<Ghe>().ToTable("Ghe");
            modelBuilder.Entity<PhongChieu>().ToTable("PhongChieu");
            modelBuilder.Entity<TheLoaiPhim>().ToTable("TheLoaiPhim");
        }
        
    }
}
