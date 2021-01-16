namespace ShopNuocHoa_Online.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("KhuyenMai")]
    public partial class KhuyenMai
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhuyenMai()
        {
            SanPhams = new HashSet<SanPham>();
        }

        [Key]
        public int MaKM { get; set; }

        [DisplayName("phần trăm khuyến mãi")]
        public int GiaKM { get; set; }

        [DisplayName("Ngày bắt đầu")]
        [DataType(DataType.Date)]
        public DateTime NgayBD { get; set; }

        [DisplayName("Ngày kết thúc")]
        [DataType(DataType.Date)]
        public DateTime NgayKT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}