namespace ShopNuocHoa_Online.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Blog")]
    public partial class Blog
    {
        [Key]
        public int MaBlog { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Ảnh")]
        public string Anh { get; set; }

        [Required]
        [StringLength(3000)]
        [DisplayName("Nội dung")]
        public string NoiDung { get; set; }

        [StringLength(250)]
        [DisplayName("Tiêu đề")]
        public string TieuDe { get; set; }
    }
}