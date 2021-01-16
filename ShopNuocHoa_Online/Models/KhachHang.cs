namespace ShopNuocHoa_Online.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("KhachHang")]
    public partial class KhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHang()
        {
            DonHangs = new HashSet<DonHang>();
        }

        [Key]
        public int MaKH { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Họ tên")]
        public string HoTen { get; set; }

        [Required]
        [StringLength(12)]
        [DisplayName("Số điện thoại")]
        [DataType(DataType.PhoneNumber)]
        public string SDT { get; set; }

        [Required]
        [StringLength(250)]
        [DisplayName("Địa chỉ")]
        public string DiaChi { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonHang> DonHangs { get; set; }
    }
}