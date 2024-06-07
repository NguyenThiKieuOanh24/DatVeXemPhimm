namespace DatVeXemPhim.Models
{
    public class KhachHang
    {
        public int iD { get; set; }
        public string hoTen { get; set; }
        public int soDienThoai { get; set; }
        public string eMail { get; set; }
        public string taiKhoan { get; set; }
        public string matKhau { get; set; }
        public ICollection<Ve> Ves { get; set; }
    }
}
