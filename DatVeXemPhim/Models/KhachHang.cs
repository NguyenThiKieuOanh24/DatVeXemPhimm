using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatVeXemPhim.Models
{
    public class KhachHang
    {
        public int id { get; set; }
        [Required]
        [Column("hoTen")]
        [Display(Name = "Họ tên")]
        [StringLength(20, MinimumLength = 2)]
        public string hoTen { get; set; }
        [Required]
        [Column("soDienThoai")]
        [Display(Name = "Số điện thoại")]
        [StringLength(11, MinimumLength = 10)]
        public string soDienThoai { get; set; }
        [Required]
        [Column("email")]
        [Display(Name = "Email")]
        [StringLength(20, MinimumLength = 10)]
        public string email { get; set; }
        [Required]
        [Column("taiKhoan")]
        [Display(Name = "Tài khoản")]
        [StringLength(20, MinimumLength = 2)]
        public string taiKhoan { get; set; }
        [Required]
        [Column("matKhau")]
        [Display(Name = "Mật khẩu")]
        [StringLength(20, MinimumLength = 2)]
        public string matKhau { get; set; }
        public ICollection<Ve>? Ves { get; set; }
    }
}
