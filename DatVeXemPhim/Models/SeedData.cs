using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DatVeXemPhim.Data;
using System;
using System.Linq;
using DatVeXemPhim.Migrations;

namespace DatVeXemPhim.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new DatVeXemPhimContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<DatVeXemPhimContext>>()))
        {
            // Look for any movies.
            if (context.KhachHang.Any())
            {
                return;   // DB has been seeded
            }

            var khachhangs = new KhachHang[]
            {
                new KhachHang 
                {
                    hoTen = "When Harry Met Sally",
                    soDienThoai = 123,
                    eMail = "Romantic Comedy",
                    taiKhoan = "asdasd",
                    matKhau = "asdsad"
                }
                };
            context.KhachHang.AddRange(khachhangs);
            context.SaveChanges();

            var phims = new Phim[]
            {
                new Phim
                {
                    tenPhim = "asdsad",
                    daoDien = "asdasd",
                    dienVien = "asdasd",
                    theLoai = "asdasd",
                    thoiGianKhoiChieu = DateTime.Parse("1959-4-15"),
                    thoiLuong = "asdasd",
                    ngonNgu = "asdasd",
                    posterPhim = "link_to_poster_2.jpg",
                },
                new Phim
                {
                    tenPhim = "asdsad",
                    daoDien = "asdasd",
                    dienVien = "asdasd",
                    theLoai = "asdasd",
                    thoiGianKhoiChieu = DateTime.Parse("2024-6-5"),
                    thoiLuong = "asdasd",
                    ngonNgu = "asdasd",
                    posterPhim = "link_to_poster_2.jpg",
                },
                new Phim
                {
                    tenPhim = "asdsad",
                    daoDien = "asdasd",
                    dienVien = "asdasd",
                    theLoai = "asdasd",
                    thoiGianKhoiChieu = DateTime.Parse("2024-6-4"),
                    thoiLuong = "asdasd",
                    ngonNgu = "asdasd",
                    posterPhim = "link_to_poster_2.jpg",
                },
                new Phim
                {
                    tenPhim = "asdsad",
                    daoDien = "asdasd",
                    dienVien = "asdasd",
                    theLoai = "asdasd",
                    thoiGianKhoiChieu = DateTime.Parse("2024-6-6"),
                    thoiLuong = "asdasd",
                    ngonNgu = "asdasd",
                    posterPhim = "link_to_poster_2.jpg",
                }
            };
            context.Phim.AddRange(phims);
            context.SaveChanges();
        }
    }
}