using PagedList;
using ShopNuocHoa_Online.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ShopNuocHoa_Online.Areas.Admin.Controllers
{
    public class SanPhamsController : Controller
    {
        private DBShopNuocHoa db = new DBShopNuocHoa();

        // GET: Admin/SanPhams
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

        // GET: Admin/SanPhams/Details/5
        public ActionResult Details(int? id)
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

        // GET: Admin/SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaDM = new SelectList(db.DanhMucs, "MaDM", "TenDM");
            ViewBag.MaKM = new SelectList(db.KhuyenMais, "MaKM", "MaKM");
            return View();
        }

        // POST: Admin/SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,MaDM,MaKM,TenSP,NhanHieu,GioiTinh,Gia,XuatXu,TgPhatHanh,NongDo,NhaPC,NhomHuong,PhongCach,MoTa,MauSac,BoSuuTap,HuongDacTrung,Anh")] SanPham sanPham)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    sanPham.Anh = "";
                    var f = Request.Files["ImageFile"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string FileName = Path.GetFileName(f.FileName);
                        string UpLoadFile = Server.MapPath("~/wwwroot/Images/") + FileName;
                        f.SaveAs(UpLoadFile);
                        sanPham.Anh = FileName;
                    }
                    db.SanPhams.Add(sanPham);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi" + ex.Message;
                ViewBag.MaDM = new SelectList(db.DanhMucs, "MaDM", "TenDM", sanPham.MaDM);
                ViewBag.MaKM = new SelectList(db.KhuyenMais, "MaKM", "MaKM", sanPham.MaKM);
                return View(sanPham);
            }
        }

        // GET: Admin/SanPhams/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.MaDM = new SelectList(db.DanhMucs, "MaDM", "TenDM", sanPham.MaDM);
            ViewBag.MaKM = new SelectList(db.KhuyenMais, "MaKM", "MaKM", sanPham.MaKM);
            return View(sanPham);
        }

        // POST: Admin/SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,MaDM,MaKM,TenSP,NhanHieu,GioiTinh,Gia,XuatXu,TgPhatHanh,NongDo,NhaPC,NhomHuong,PhongCach,MoTa,MauSac,BoSuuTap,HuongDacTrung,Anh")] SanPham sanPham)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var f = Request.Files["ImageFile"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string FileName = Path.GetFileName(f.FileName);
                        string UpLoadFile = Server.MapPath("~/wwwroot/Images/") + FileName;
                        f.SaveAs(FileName);
                        sanPham.Anh = FileName;
                    }
                    db.Entry(sanPham).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi" + ex.Message;
                ViewBag.MaDM = new SelectList(db.DanhMucs, "MaDM", "TenDM", sanPham.MaDM);
                ViewBag.MaKM = new SelectList(db.KhuyenMais, "MaKM", "MaKM", sanPham.MaKM);
                return View(sanPham);
            }
        }

        // GET: Admin/SanPhams/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Admin/SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            try
            {
                db.SanPhams.Remove(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi" + ex.Message;
                return View(sanPham);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}