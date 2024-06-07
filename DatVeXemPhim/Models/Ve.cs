namespace DatVeXemPhim.Models
{
    public class Ve
    {
        public int iD { get; set; }
        public int maXuatChieu { get; set; }
        public int maKhachHang { get; set; }
        public int maNhanVien { get; set; }
        public int maGhe { get; set; }
        public DateTime ngayBanVe { get; set; }
        public int tongTien { get; set; }

        public KhachHang KhachHang { get; set; }
        public NhanVien NhanVien { get; set; }
        public Ghe Ghe { get; set; }
        public XuatChieu XuatChieu { get; set; }
    }
}
