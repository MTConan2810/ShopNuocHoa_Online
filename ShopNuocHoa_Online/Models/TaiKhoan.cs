namespace ShopNuocHoa_Online.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TaiKhoan")]
    public partial class TaiKhoan
    {
        [Key]
        public int MaTK { get; set; }

        [Required]
        [DisplayName("Họ tên:")]
        [StringLength(50)]
        public string HoTen { get; set; }

        [Required]
        [DisplayName("Email:"), DataType(DataType.EmailAddress)]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(100)]
        [DisplayName("Địa chỉ")]
        public string DiaChi { get; set; }

        [StringLength(12)]
        [DisplayName("Số điện thoại")]
        public string SDT { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Tên đăng nhập phải có ít nhất 3 kí tự")]
        [DisplayName("Tên đăng nhập")]
        [StringLength(50)]
        public string TenDangNhap { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Tối thiểu 3 kí tự,Tối đa 20 kí tự")]
        [DisplayName("Mật Khẩu"), DataType(DataType.Password)]
        public string MatKhau { get; set; }

        [StringLength(30)]
        [DisplayName("Quyền")]
        public string Quyen { get; set; }
    }
}