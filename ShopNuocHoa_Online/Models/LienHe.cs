namespace ShopNuocHoa_Online.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("LienHe")]
    public partial class LienHe
    {
        [Key]
        public int MaLH { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Họ tên")]
        public string HoTenKH { get; set; }

        [StringLength(30)]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(12)]
        [DisplayName("Số điện thoại")]
        [DataType(DataType.PhoneNumber)]
        public string SDT { get; set; }

        [Required]
        [StringLength(250)]
        [DisplayName("Địa chỉ")]
        public string DiaChi { get; set; }

        [StringLength(250)]
        [DisplayName("Nội dung")]
        public string NoiDung { get; set; }
    }
}