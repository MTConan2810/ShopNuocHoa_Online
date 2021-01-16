using ShopNuocHoa_Online.Models;

namespace ShopNuocHoa_Online.Dao
{
    public class CTDHDao
    {
        private DBShopNuocHoa db = null;

        public CTDHDao()
        {
            db = new DBShopNuocHoa();
        }

        public bool Them(ChiTietDH ctdh)
        {
            try
            {
                db.ChiTietDHs.Add(ctdh);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}