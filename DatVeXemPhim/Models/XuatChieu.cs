using System.ComponentModel.DataAnnotations.Schema;

namespace DatVeXemPhim.Models
{
    public class XuatChieu
    {
        public int iD { get; set; }
        public int maPhong { get; set; }
        public int maPhim { get; set; }
        [ForeignKey("maPhim")]
        public Phim Phim { get; set; }
        public DateTime ngayChieu { get; set; }
        public DateTime gioBatDau { get; set; }
        public DateTime gioKetThuc { get; set; }
    }
}
