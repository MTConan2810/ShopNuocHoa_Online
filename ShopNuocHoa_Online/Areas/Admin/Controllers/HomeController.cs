using ShopNuocHoa_Online.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ShopNuocHoa_Online.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private DBShopNuocHoa db = new DBShopNuocHoa();

        // GET: Admin/Home
        public ActionResult Index()
        {
            if (Session["idUserAD"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("DangNhap");
            }
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(string TenDangNhap, string MatKhau)
        {
            if (ModelState.IsValid)
            {
                var user = db.TaiKhoans.Where(u => u.TenDangNhap.Equals(TenDangNhap) && u.MatKhau.Equals(MatKhau)).ToList();
                if (user.Count() > 0)
                {
                    if (user.Select(u => u.Quyen).Contains("Admin"))
                    {
                        Session["HoTen"] = user.FirstOrDefault().HoTen;
                        Session["Email"] = user.FirstOrDefault().Email;
                        Session["idUserAD"] = user.FirstOrDefault().MaTK;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.error = "Bạn không có quyền truy cập, vui lòng đăng nhập lại";
                    }
                }
                else
                {
                    ViewBag.error = "Tài khoản hoặc mật khẩu sai! Vui lòng đăng nhập lại!!!!";
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("DangNhap");
        }
    }
}