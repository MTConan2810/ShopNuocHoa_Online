using ShopNuocHoa_Online.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ShopNuocHoa_Online.Areas.Admin.Controllers
{
    public class ChiTietDHsController : Controller
    {
        private DBShopNuocHoa db = new DBShopNuocHoa();

        // GET: Admin/ChiTietDHs
        public ActionResult Index()
        {
            var chiTietDHs = db.ChiTietDHs.Include(c => c.DonHang).Include(c => c.SanPham);
            return View(chiTietDHs.ToList());
        }

        // GET: Admin/ChiTietDHs/Details/5
        public ActionResult Details(int? id, int? idsp)
        {
            if (id == null && idsp == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDH chiTietDH = db.ChiTietDHs.Find(id, idsp);
            if (chiTietDH == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDH);
        }

        // GET: Admin/ChiTietDHs/Create
        public ActionResult Create()
        {
            ViewBag.MaDH = new SelectList(db.DonHangs, "MaDH", "DiaChiGH");
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            return View();
        }

        // POST: Admin/ChiTietDHs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,MaDH,SoLuongBan")] ChiTietDH chiTietDH)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietDHs.Add(chiTietDH);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDH = new SelectList(db.DonHangs, "MaDH", "DiaChiGH", chiTietDH.MaDH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", chiTietDH.MaSP);
            return View(chiTietDH);
        }

        // GET: Admin/ChiTietDHs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDH chiTietDH = db.ChiTietDHs.Find(id);
            if (chiTietDH == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDH = new SelectList(db.DonHangs, "MaDH", "DiaChiGH", chiTietDH.MaDH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", chiTietDH.MaSP);
            return View(chiTietDH);
        }

        // POST: Admin/ChiTietDHs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,MaDH,SoLuongBan")] ChiTietDH chiTietDH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietDH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDH = new SelectList(db.DonHangs, "MaDH", "DiaChiGH", chiTietDH.MaDH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", chiTietDH.MaSP);
            return View(chiTietDH);
        }

        // GET: Admin/ChiTietDHs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDH chiTietDH = db.ChiTietDHs.Find(id);
            if (chiTietDH == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDH);
        }

        // POST: Admin/ChiTietDHs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietDH chiTietDH = db.ChiTietDHs.Find(id);
            db.ChiTietDHs.Remove(chiTietDH);
            db.SaveChanges();
            return RedirectToAction("Index");
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