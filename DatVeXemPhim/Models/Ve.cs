using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatVeXemPhim.Models
{
    public class Ve
    {
        public int id { get; set; }
        
        public int maXuatChieu { get; set; }
        [ForeignKey("maXuatChieu")]
        [Display(Name = "Mã xuất chiếu")]
        public XuatChieu? fk_XuatChieu { get; set; }

        public int maKhachHang { get; set; }
        [ForeignKey("maKhachHang")]
        [Display(Name = "Mã khách hàng")]
        public KhachHang? fk_KhachHang { get; set; }

        public int maNhanVien { get; set; }
        [Display(Name = "Mã nhân viên")]
        [ForeignKey("maNhanVien")]
        public NhanVien? fk_NhanVien { get; set; }

        public int maGhe { get; set; }
        [Display(Name = "Mã ghế")]
        [ForeignKey("maGhe")]
        public Ghe? fk_MaGhe { get; set; }

        [Display(Name = "Ngày bán vé")]
        public DateTime ngayBanVe { get; set; }

        [Display(Name = "Tổng tiền")]
        public int tongTien { get; set; }
    }
}
