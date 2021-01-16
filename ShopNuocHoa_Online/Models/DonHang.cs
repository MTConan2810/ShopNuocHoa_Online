namespace ShopNuocHoa_Online.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DonHang")]
    public partial class DonHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonHang()
        {
            ChiTietDHs = new HashSet<ChiTietDH>();
        }

        [Key]
        public int MaDH { get; set; }

        [DisplayName("Ngày Lập")]
        [DataType(DataType.Date)]
        public DateTime NgayLap { get; set; }

        [Required]
        [StringLength(250)]
        [DisplayName("Địa chỉ giao hàng")]
        public string DiaChiGH { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Hình thức thanh toán")]
        public string HinhThucTT { get; set; }

        [DisplayName("Tổng Tiền")]
        public int TongTien { get; set; }

        [StringLength(250)]
        [DisplayName("Ghi Chú, trạng thái")]
        public string GhiChu { get; set; }

        public int MaKH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDH> ChiTietDHs { get; set; }

        public virtual KhachHang KhachHang { get; set; }
    }
}