using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAPP.Models;

namespace WebAPP.Controllers
{
    public class SanPhamsController : Controller
    {
        private FreshEntities db = new FreshEntities();

        // GET: SanPhams
        public ActionResult Index()
        {
            var sanPham = db.SanPham.Include(s => s.LoaiSp).Include(s => s.NhaCungCap1);
            return View(sanPham.ToList());
        }

        // GET: SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaLoai = new SelectList(db.LoaiSp, "MaLoai", "TenLoai");
            ViewBag.NhaCungCap = new SelectList(db.NhaCungCap, "MaNCC", "TenNCC");
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,GiaBan,MoTa,HinhAnh,NgayCapNhat,SoLuongCon,MaLoai,NhaCungCap,HanSuDung")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                sanPham.NgayCapNhat = DateTime.Today;
                db.SanPham.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoai = new SelectList(db.LoaiSp, "MaLoai", "TenLoai", sanPham.MaLoai);
            ViewBag.NhaCungCap = new SelectList(db.NhaCungCap, "MaNCC", "TenNCC", sanPham.NhaCungCap);
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            //var TenMaLoai = from sp in db.SanPham
            //                join lsp in db.LoaiSp
            //                on sp.MaLoai equals lsp.MaLoai
            //                select lsp.TenLoai;
            //var TenNCC = from sp in db.SanPham
            //             join ncc in db.NhaCungCap
            //             on sp.NhaCungCap equals ncc.MaNCC
            //             select ncc.TenNCC;

            //ViewBag.MaLoai = new SelectList(TenMaLoai);
            //ViewBag.NhaCungCap = new SelectList(TenNCC);
            sanPham.NgayCapNhat = DateTime.Today;
            ViewBag.MaLoai = new SelectList(db.LoaiSp, "MaLoai", "TenLoai", sanPham.MaLoai);
            ViewBag.NhaCungCap = new SelectList(db.NhaCungCap, "MaNCC", "TenNCC", sanPham.NhaCungCap);
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,GiaBan,MoTa,NgayCapNhat,SoLuongCon,MaLoai,NhaCungCap,HanSuDung")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //var TenMaLoai = from sp in db.SanPham
            //                join lsp in db.LoaiSp
            //                on sp.MaLoai equals lsp.MaLoai
            //                select lsp.TenLoai;
            //var TenNCC = from sp in db.SanPham
            //             join ncc in db.NhaCungCap
            //             on sp.NhaCungCap equals ncc.MaNCC
            //             select ncc.TenNCC;

            //ViewBag.MaLoai = new SelectList(TenMaLoai);
            //ViewBag.NhaCungCap = new SelectList(TenNCC);
            ViewBag.MaLoai = new SelectList(db.LoaiSp, "MaLoai", "TenLoai", sanPham.MaLoai);
            ViewBag.NhaCungCap = new SelectList(db.NhaCungCap, "MaNCC", "TenNCC", sanPham.NhaCungCap);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPham.Find(id);
            db.SanPham.Remove(sanPham);
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
