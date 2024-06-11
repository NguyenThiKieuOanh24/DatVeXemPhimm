using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatVeXemPhim.Models
{
    public class Ve
    {
        public int id { get; set; }

        public int maXuatChieu { get; set; }
        [ForeignKey("maXuatChieu")]
        public XuatChieu? fk_XuatChieu { get; set; }

        public int maKhachHang { get; set; }
        [ForeignKey("maKhachHang")]
        public KhachHang? fk_KhachHang { get; set; }
        public int maNhanVien { get; set; }
        [ForeignKey("maNhanVien")]
        public NhanVien? fk_NhanVien { get; set; }
        public int maGhe { get; set; }
        [ForeignKey("maGhe")]
        public Ghe? fk_MaGhe { get; set; }
        public DateTime ngayBanVe { get; set; }
        public int tongTien { get; set; }
    }
}
