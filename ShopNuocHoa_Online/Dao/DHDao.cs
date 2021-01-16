using ShopNuocHoa_Online.Models;

namespace ShopNuocHoa_Online.Dao
{
    public class DHDao
    {
        private DBShopNuocHoa db = null;

        public DHDao()
        {
            db = new DBShopNuocHoa();
        }

        public long Them(DonHang dh)
        {
            db.DonHangs.Add(dh);
            db.SaveChanges();
            return dh.MaDH;
        }
    }
}