namespace DatVeXemPhim.Models
{
    public class KhachHang
    {
        public int id { get; set; }
        public string hoTen { get; set; }
        public string soDienThoai { get; set; }
        public string email { get; set; }
        public string taiKhoan { get; set; }
        public string matKhau { get; set; }
        public ICollection<Ve>? Ves { get; set; }
    }
}
