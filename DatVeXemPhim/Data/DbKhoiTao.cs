using DatVeXemPhim.Models;

namespace DatVeXemPhim.Data
{
    public class DbKhoiTao
    {
        public static void khoiTao(DatVeXemPhimContext context)
        {
            context.Database.EnsureCreated();

            // Look for any khach hang.
            if (context.KhachHang.Any())
            {
                return;   // DB has been seeded
            }

            var khachHangs = new KhachHang[]
            {
             new KhachHang{hoTen="Anh Duc",soDienThoai="123",email="duc@gmail.com",taiKhoan="duc123",matKhau="123"},
             new KhachHang{hoTen="Anh Teo",soDienThoai="124",email="teo@gmail.com",taiKhoan="teo124",matKhau="124"},
            };
            foreach (KhachHang s in khachHangs)
            {
                context.KhachHang.Add(s);
            }
            context.SaveChanges();

            var ves = new Ve[]
            {
                 new Ve{maGhe=1,maKhachHang=1,maNhanVien=1,maXuatChieu=1,ngayBanVe=DateTime.Parse("2024-09-01"),tongTien=10},
                 new Ve{maGhe=2,maKhachHang=2,maNhanVien=2,maXuatChieu=2,ngayBanVe=DateTime.Parse("2024-12-02"),tongTien=15},
                };
            foreach (Ve h in ves)
            {
                context.Ve.Add(h);
            }
            context.SaveChanges();



            var nhanViens = new NhanVien[]
            {
             new NhanVien{hoTen="Anh Nam",diaChi="321 abc",soDienThoai=321,email="nam@gmail.com",ngaySinh=DateTime.Parse("2024-09-01"),gioiTinh="nam",taiKhoan="nam321",matKhau="321"},
             new NhanVien{hoTen="Anh Ti",diaChi="421 abc",soDienThoai=421,email="ti@gmail.com",ngaySinh=DateTime.Parse("2024-12-01"),gioiTinh="nu",taiKhoan="ti421",matKhau="421"},
            };
            foreach (NhanVien c in nhanViens)
            {
                context.NhanVien.Add(c);
            }
            context.SaveChanges();

            var xuatChieus = new XuatChieu[]
            {
             new XuatChieu{id=1,ngayChieu=DateTime.Parse("2024-09-01"),gioBatDau=DateTime.Parse("2024-09-01"),gioKetThuc=DateTime.Parse("2024-09-01")},
             new XuatChieu{id=2,ngayChieu=DateTime.Parse("2024-12-02"),gioBatDau=DateTime.Parse("2024-12-02"),gioKetThuc=DateTime.Parse("2024-12-02")},
            };
            foreach (XuatChieu e in xuatChieus)
            {
                context.XuatChieu.Add(e);
            }
            context.SaveChanges();

            var ghes = new Ghe[]
            {
             new Ghe{id=1,tenGhe="G2"},
             new Ghe{id=2,tenGhe="E3"},
            };
            foreach (Ghe g in ghes)
            {
                context.Ghe.Add(g);
            }
            context.SaveChanges();




        }
    }
}
