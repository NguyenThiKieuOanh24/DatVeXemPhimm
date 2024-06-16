using System.ComponentModel.DataAnnotations.Schema;

namespace DatVeXemPhim.Models
{
    public class Ghe
    {
        public int id { get; set; }
        public int maPhong { get; set; }
        [ForeignKey("maPhong")]
        public PhongChieu? fk_PhongChieu { get; set; }
        public string? tenGhe { get; set; }
        public ICollection<Ve>? Ves { get; set; }
    }
}
