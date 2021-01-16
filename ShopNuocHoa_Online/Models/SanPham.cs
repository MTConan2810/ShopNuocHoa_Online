namespace ShopNuocHoa_Online.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            BinhLuans = new HashSet<BinhLuan>();
            ChiTietDHs = new HashSet<ChiTietDH>();
        }

        [Key]
        public int MaSP { get; set; }

        public int MaDM { get; set; }

        public int? MaKM { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Tên sản phẩm:")]
        public string TenSP { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Nhãn hiệu:")]
        public string NhanHieu { get; set; }

        [StringLength(10)]
        [DisplayName("Giới tính:")]
        public string GioiTinh { get; set; }

        [DisplayName("Giá:")]
        public double Gia { get; set; }

        [StringLength(50)]
        [DisplayName("Xuất sứ:")]
        public string XuatXu { get; set; }

        [DisplayName("Thời gian phát hành:")]
        [DataType(DataType.Date)]
        public DateTime? TgPhatHanh { get; set; }

        [StringLength(30)]
        [DisplayName("Nồng độ:")]
        public string NongDo { get; set; }

        [StringLength(50)]
        [DisplayName("Nhà Pha chế:")]
        public string NhaPC { get; set; }

        [StringLength(50)]
        [DisplayName("Nhóm hương:")]
        public string NhomHuong { get; set; }

        [StringLength(50)]
        [DisplayName("Phong cách:")]
        public string PhongCach { get; set; }

        [StringLength(3000)]
        [DisplayName("Mô tả:")]
        public string MoTa { get; set; }

        [StringLength(50)]
        [DisplayName("Màu sắc:")]
        public string MauSac { get; set; }

        [StringLength(50)]
        [DisplayName("Bộ sưu tập:")]
        public string BoSuuTap { get; set; }

        [StringLength(250)]
        [DisplayName("Hương đặc trưng:")]
        public string HuongDacTrung { get; set; }

        [StringLength(50)]
        [DisplayName("Ảnh:")]
        public string Anh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDH> ChiTietDHs { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }

        public virtual KhuyenMai KhuyenMai { get; set; }
    }
}