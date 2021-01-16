using ShopNuocHoa_Online.Models;

namespace ShopNuocHoa_Online.Dao
{
    public class KHDao
    {
        private DBShopNuocHoa db = null;

        public KHDao()
        {
            db = new DBShopNuocHoa();
        }

        public long Them(KhachHang kh)
        {
            db.KhachHangs.Add(kh);
            db.SaveChanges();
            return kh.MaKH;
        }
    }
}