using System.ComponentModel.DataAnnotations.Schema;

namespace DatVeXemPhim.Models
{
    public class XuatChieu
    {
        public int id { get; set; }
        public int maPhong { get; set; }
        [ForeignKey("maPhong")]
        public PhongChieu? fk_PhongChieu { get; set; }
        public int maPhim { get; set; }
        [ForeignKey("maPhim")]
        public Phim? fk_Phim { get; set; }

        public DateTime ngayChieu { get; set; }
        public DateTime gioBatDau { get; set; }
        public DateTime gioKetThuc { get; set; }
        public ICollection<Ve>? Ves { get; set; }
    }
}
