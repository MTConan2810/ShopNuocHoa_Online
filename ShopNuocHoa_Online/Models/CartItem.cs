using System.ComponentModel.DataAnnotations;

namespace ShopNuocHoa_Online.Models
{
    public class CartItem
    {
        [Range(minimum: 1, maximum: 200)]
        public int quantity { set; get; }

        public SanPham sanpham { set; get; }
    }
}