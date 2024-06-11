using System.ComponentModel.DataAnnotations.Schema;

namespace DatVeXemPhim.Models
{
    public class Phim
    {
        public int id { get; set; }
        public string? posterPhim { get; set; }
        public string tenPhim { get; set; }
        public string daoDien { get; set; }
        public string dienVien { get; set; }
        public int maLoaiPhim { get; set; }
        [ForeignKey("maLoaiPhim")]
        public TheLoaiPhim? fk_TheLoaiPhim { get; set; }
        public DateTime thoiGianKhoiChieu { get; set; }
        public string thoiLuong { get; set; }
        public string ngonNgu { get; set; }
        public ICollection<XuatChieu>? XuatChieus { get; set; }
    }
}
