namespace DatVeXemPhim.Models
{
    public class PhongChieu
    {
        public int id { get; set; }
        public string tenPhong { get; set; }
        public ICollection<XuatChieu> XuatChieus { get; set; }
    }
}
