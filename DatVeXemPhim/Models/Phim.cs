using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatVeXemPhim.Models
{
    public class Phim
    {
        public int id { get; set; }
        [DisplayName("Ảnh phim")]
        public string? posterPhim { get; set; }

        [Required]
        [DisplayName("Tên phim")]
        public string? tenPhim { get; set; }

        [DisplayName("Đạo diễn")]
        public string? daoDien { get; set; }

        [DisplayName("Diễn viên")]
        public string? dienVien { get; set; }

        [Required]
        [DisplayName("Mã loại phim")]
        public int maLoaiPhim { get; set; }
        [ForeignKey("maLoaiPhim")]
        public virtual TheLoaiPhim? fk_TheLoaiPhim { get; set; }

        [DisplayName("Thời gian khởi chiếu")]
        public DateTime thoiGianKhoiChieu { get; set; }

        [DisplayName("Thời lượng")]

        public string? thoiLuong { get; set; }

        [DisplayName("Ngôn ngữ")]
        public string? ngonNgu { get; set; }
        public ICollection<XuatChieu>? XuatChieus { get; set; }
    }
}
