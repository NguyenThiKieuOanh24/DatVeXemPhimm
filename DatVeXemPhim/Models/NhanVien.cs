using System.ComponentModel.DataAnnotations;

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

        [Required]
        [MaxLength(20, ErrorMessage = "Tài khoản chỉ tối đa 20 kí tự")]
        public string taiKhoan { get; set; }

        [Required]
        public string matKhau { get; set; }
        public ICollection<Ve>? Ves { get; set; }
    }
}
