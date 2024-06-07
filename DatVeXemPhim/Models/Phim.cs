namespace DatVeXemPhim.Models
{
    public class Phim
    {
        public int iD { get; set; }
        public string posterPhim { get; set; }
        public string tenPhim { get; set; }
        public string daoDien { get; set; }
        public string dienVien { get; set; }
        public string theLoai { get; set; }
        public DateTime thoiGianKhoiChieu { get; set; }
        public string thoiLuong { get; set; }
        public string ngonNgu { get; set; }

        public TheLoaiPhim TheLoaiPhim { get; set; }
    }
}
