namespace ShopNuocHoa_Online.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("BinhLuan")]
    public partial class BinhLuan
    {
        [Key]
        public int MaBL { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Họ tên")]
        public string HoTen { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(250)]
        [DisplayName("Nội dung")]
        public string NoiDung { get; set; }

        [DisplayName("Thời gian")]
        public DateTime? ThoiGian { get; set; }

        public int MaSP { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}