namespace DatVeXemPhim.Models
{
    public class Ghe
    {
        public int iD { get; set; }
        public int maPhong { get; set; }
        public string tenGhe { get; set; }
        public ICollection<Ve> Ves { get; set; }
    }
}
