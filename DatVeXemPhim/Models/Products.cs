using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatVeXemPhim.Models
{
    [Table("Products")]
    public class Products
    {
        [Key]
        public int id { get; set; }

        public string tenPhim { get; set; }

        public string daoDien { get; set; }

        public string dienVien { get; set; }

        public int theLoai { get; set; }

        public DateTime thoiGianKhoiChieu { get; set; }
    }
}
