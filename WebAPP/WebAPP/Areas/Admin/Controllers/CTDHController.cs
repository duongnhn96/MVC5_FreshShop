using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAPP.Dao;
using WebAPP.Models;
using PagedList;
namespace WebAPP.Areas.Admin.Controllers
{
    public class CTDHController : BaseController
    {
        private FreshEntities db = new FreshEntities();

        // GET: Admin/CTDH

        public ActionResult Index(int page = 1, int pageSize = 11)
        {
            var dao = new DonHangDao();
            var model = dao.ListAllPaging2(page, pageSize);
            //var chiTietDonHang = db.ChiTietDonHang.Include(c => c.DonHang).Include(c => c.SanPham);
            return View(model);
        }

        // GET: Admin/CTDH/Details/5
        public ActionResult Details(int? idDH, int ? idSP)
        {
            if (idDH == null || idSP ==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDonHang chiTietDonHang = db.ChiTietDonHang.Where(x => x.MaDH==idDH && x.MaSP==idSP).SingleOrDefault();
            if (chiTietDonHang == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDonHang);
        }

        // GET: Admin/CTDH/Create
        public ActionResult Create()
        {
            ViewBag.MaDH = new SelectList(db.DonHang, "MaDH", "LuuY");
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "TenSP");
            return View();
        }

        // POST: Admin/CTDH/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDH,MaSP,SoLuong,DonGia")] ChiTietDonHang chiTietDonHang)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietDonHang.Add(chiTietDonHang);
                db.SaveChanges();
                Url.Action("TongTienDH", "DonHang", new { maDH = chiTietDonHang.MaDH });
                return RedirectToAction("Details", "DonHang", new { id = chiTietDonHang.MaDH });
            }

            ViewBag.MaDH = new SelectList(db.DonHang, "MaDH", "LuuY", chiTietDonHang.MaDH);
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "TenSP", chiTietDonHang.MaSP);
            return View(chiTietDonHang);
        }

        // GET: Admin/CTDH/Edit/5
        public ActionResult Edit(int? idDH, int? idSP)
        {
            if (idDH == null || idSP == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDonHang chiTietDonHang = db.ChiTietDonHang.Where(x => x.MaDH == idDH && x.MaSP == idSP).SingleOrDefault();
            if (chiTietDonHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDH = new SelectList(db.DonHang, "MaDH", "LuuY", chiTietDonHang.MaDH);
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "TenSP", chiTietDonHang.MaSP);
            return View(chiTietDonHang);
        }

        // POST: Admin/CTDH/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDH,MaSP,SoLuong,DonGia")] ChiTietDonHang chiTietDonHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietDonHang).State = EntityState.Modified;
                db.SaveChanges();
                Url.Action("TongTienDH", "DonHang", new { maDH = chiTietDonHang.MaDH });
                return RedirectToAction("Details", "DonHang", new { id = chiTietDonHang.MaDH });
            }
            ViewBag.MaDH = new SelectList(db.DonHang, "MaDH", "LuuY", chiTietDonHang.MaDH);
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "TenSP", chiTietDonHang.MaSP);
   
            return View(chiTietDonHang);
        }


        public ActionResult Delete(int?idDH, int ? idSP)
        {
            if (idDH == null || idSP == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDonHang chiTietDonHang = db.ChiTietDonHang.Where(x => x.MaDH == idDH && x.MaSP == idSP).SingleOrDefault();
            if (chiTietDonHang == null)
            {
                return HttpNotFound();
            }
            int madh = chiTietDonHang.MaDH;
            db.ChiTietDonHang.Remove(chiTietDonHang);
            db.SaveChanges();
            Url.Action("TongTienDH", "DonHang", new { maDH = madh });
            return RedirectToAction("Details", "DonHang", new { id = chiTietDonHang.MaDH });
        }
    }
}
