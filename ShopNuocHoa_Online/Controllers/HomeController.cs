using PagedList;
using ShopNuocHoa_Online.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ShopNuocHoa_Online.Controllers
{
    public class HomeController : Controller
    {
        private DBShopNuocHoa db = new DBShopNuocHoa();

        public ActionResult Index(string id, string sortOder, string searchString, string currentFilter, int? page)
        {
            List<SanPham> sanPhams = new List<SanPham>();
            ViewBag.CurrentSort = sortOder;//lấy yêu cầu sắp
            ViewBag.SapTheoTen = String.IsNullOrEmpty(sortOder) ? "name_desc" : "";
            ViewBag.SapTheoGia = sortOder == "Gia" ? "gia_desc" : "Gia";
            ViewBag.SapTheoNgay = sortOder == "Ngay" ? "ngay_desc" : "Ngay";
            if (id == null)
            {
                sanPhams = db.SanPhams.Select(s => s).ToList();
            }
            else
            {
                sanPhams = db.SanPhams.Where(s => s.DanhMuc.TenDM.Contains(id)).Select(s => s).ToList();
            }
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                sanPhams = db.SanPhams.Where(s => s.TenSP.Contains(searchString) || s.NhanHieu.Contains(searchString)).Select(s => s).ToList();
            }
            //sắp xếp
            switch (sortOder)
            {
                case "name_desc":
                    sanPhams = sanPhams.OrderByDescending(s => s.TenSP).ToList();
                    break;

                case "Gia":
                    sanPhams = sanPhams.OrderBy(s => s.Gia).ToList();
                    break;

                case "gia_desc":
                    sanPhams = sanPhams.OrderByDescending(s => s.Gia).ToList();
                    break;

                case "Ngay":
                    sanPhams = sanPhams.OrderBy(s => s.TgPhatHanh).ToList();
                    break;

                case "ngay_desc":
                    sanPhams = sanPhams.OrderByDescending(s => s.TgPhatHanh).ToList();
                    break;

                default:
                    sanPhams = sanPhams.OrderBy(s => s.TenSP).ToList();
                    break;
            }
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(sanPhams.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult SanPham_KM()
        {
            return View();
        }

        public PartialViewResult _BinhLuan(string bl, int? page)
        {
            List<BinhLuan> binhLuans = new List<BinhLuan>();
            if (bl == null)
            {
                binhLuans = db.BinhLuans.Select(b => b).ToList();
            }
            else
            {
                binhLuans = db.BinhLuans.Where(b => b.SanPham.TenSP.Contains(bl)).Select(b => b).ToList();
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return PartialView(binhLuans.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ChiTietSP(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        [HttpGet]
        public ActionResult Register()
        {
            List<string> Cities = new List<string>
            {
                "Hà Nội","Thanh Hoá","Thái Bình","Nam Định"
            };
            ViewBag.Cities = Cities;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "MaTK,HoTen,Email,DiaChi,SDT,TenDangNhap,MatKhau,Quyen")] TaiKhoan taiKhoan)
        {
            var tkcheck = db.TaiKhoans.Where(tk => tk.Email == taiKhoan.Email || tk.TenDangNhap == taiKhoan.TenDangNhap);
            if (tkcheck.Count() == 0)
            {
                if (ModelState.IsValid)
                {
                    db.TaiKhoans.Add(taiKhoan);
                    db.SaveChanges();
                }
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.error = "Tài khoản đã tồn tại";
                return View(taiKhoan);
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string TenDangNhap, string MatKhau)
        {
            if (ModelState.IsValid)
            {
                var user = db.TaiKhoans.Where(u => u.TenDangNhap.Equals(TenDangNhap) && u.MatKhau.Equals(MatKhau)).ToList();
                if (user.Count() > 0)
                {
                    //add session
                    Session["HoTen"] = user.FirstOrDefault().HoTen;
                    Session["Email"] = user.FirstOrDefault().Email;
                    Session["idUser"] = user.FirstOrDefault().MaTK;
                    Session["SDT"] = user.FirstOrDefault().SDT;
                    Session["DiaChi"] = user.FirstOrDefault().DiaChi;
                    return RedirectToAction("Index");
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
            return RedirectToAction("Login");
        }

        public PartialViewResult _Nav()
        {
            var danhmuc = db.DanhMucs.Select(n => n);
            return PartialView(danhmuc);
        }

        public PartialViewResult _Searchbar()
        {
            var sanPhams = db.SanPhams.Select(n => n);
            return PartialView(sanPhams);
        }

        public ActionResult Blog()
        {
            return View(db.Blogs.ToList());
        }

        public ActionResult ChiTietBlogs(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        private string CartSession = "CartSession";

        [ChildActionOnly]
        public PartialViewResult _CartView()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }

        public ActionResult UserTT(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // POST: Admin/TaiKhoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserTT([Bind(Include = "MaTK,HoTen,Email,DiaChi,SDT,TenDangNhap,MatKhau,Quyen")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taiKhoan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taiKhoan);
        }

        public ActionResult SanPhamKM()
        {
            var sanPhams = db.SanPhams.Where(s => s.MaKM != null);
            return View(sanPhams.ToList());
        }
    }
}