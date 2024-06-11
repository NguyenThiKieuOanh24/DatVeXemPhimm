namespace DatVeXemPhim.Models
{
    public class NhanVien
    {
        public int id { get; set; }
        public string hoTen { get; set; }
        public string diaChi { get; set; }
        public int soDienThoai { get; set; }
        public string email { get; set; }
        public DateTime ngaySinh { get; set; }
        public string gioiTinh { get; set; }
        public string taiKhoan { get; set; }
        public string matKhau { get; set; }
        public ICollection<Ve>? Ves { get; set; }
    }
}
