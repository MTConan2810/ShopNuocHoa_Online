using ShopNuocHoa_Online.Dao;
using ShopNuocHoa_Online.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ShopNuocHoa_Online.Controllers
{
    public class CartController : Controller
    {
        private DBShopNuocHoa db = new DBShopNuocHoa();
        private static string CartSession = "CartSession";

        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        public ActionResult AddItem(int quantity, int masp)
        {
            var sp = db.SanPhams.Where(s => s.MaSP == masp).FirstOrDefault();
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.sanpham.MaSP == sp.MaSP))
                {
                    foreach (var item in list)
                    {
                        item.quantity += quantity;
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.sanpham = sp;
                    item.quantity = quantity;
                    list.Add(item);
                }
                Session[CartSession] = list;
            }
            else
            {
                var item = new CartItem();
                item.sanpham = sp;
                item.quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //gán vào session
                Session[CartSession] = list;
            }
            return RedirectToAction("Index", "Home");
        }

        /*-----Update----*/

        [Route("/updatecart", Name = "updatecart")]
        [HttpPost]
        public ActionResult UpdateCart(int masp, int quantity)
        {
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = Session[CartSession];
            var list = (List<CartItem>)cart;
            var cartItem = list.Find(p => p.sanpham.MaSP == masp);
            if (cartItem != null)
            {
                if (quantity > 0)
                {
                    cartItem.quantity = quantity;
                }
                else
                {
                    ViewBag.error = "--Lỗi-- số lượng phải lớn hơn 0 và là số";
                }
            }
            Session[CartSession] = list;
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return RedirectToAction("Index");
        }

        public ActionResult RemoveCart(int masp)
        {
            var cart = Session[CartSession];
            var list = (List<CartItem>)cart;
            var cartItem = list.Find(p => p.sanpham.MaSP == masp);
            if (cartItem != null)
            {
                list.Remove(cartItem);
            }
            Session[CartSession] = list;
            return RedirectToAction("Index");
        }

        public ActionResult PayMent()
        {
            List<string> htttoan = new List<string>
            {
                "Tiền mặt","Thẻ"
            };
            ViewBag.hthuc = htttoan;
            return View();
        }

        // POST: Admin/DonHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult PayMent(string hoten, string diachi, string email, string sdt, string ghichu, string hinhthucpay)
        {
            var cart = Session[CartSession];
            var list = (List<CartItem>)cart;

            KhachHang kh = new KhachHang();
            kh.MaKH = db.KhachHangs.Count() + 1;
            kh.HoTen = hoten;
            kh.DiaChi = diachi;
            kh.Email = email;
            kh.SDT = sdt;
            //try catch
            var makh = new KHDao().Them(kh);
            DonHang dh = new DonHang();
            dh.MaDH = db.DonHangs.Count() + 1;
            dh.MaKH = (int)makh;
            dh.HinhThucTT = hinhthucpay;
            dh.GhiChu = ghichu;
            dh.DiaChiGH = diachi;
            dh.NgayLap = DateTime.Now;
            foreach (var item in list)
            {
                if (item.sanpham.MaKM != null)
                {
                    var thanhtien = (item.sanpham.Gia - item.sanpham.Gia * item.sanpham.KhuyenMai.GiaKM / 100) * item.quantity;
                    dh.TongTien += Convert.ToInt32(thanhtien);
                }
                else
                {
                    var thanhtien = item.sanpham.Gia * item.quantity;
                    dh.TongTien += Convert.ToInt32(thanhtien);
                }
            }
            try
            {
                var madh = new DHDao().Them(dh);
                var detailDH = new CTDHDao();
                foreach (var item in list)
                {
                    ChiTietDH ctdh = new ChiTietDH();
                    ctdh.MaDH = (int)madh;
                    ctdh.MaSP = item.sanpham.MaSP;
                    ctdh.SoLuongBan = item.quantity;
                    detailDH.Them(ctdh);
                }
            }
            catch (Exception ex)
            {
                ViewBag.error = "Lỗi" + ex.Message;
            }

            return Redirect("Success");
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}