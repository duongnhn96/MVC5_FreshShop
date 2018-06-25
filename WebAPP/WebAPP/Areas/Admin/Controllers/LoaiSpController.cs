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

namespace WebAPP.Areas.Admin.Controllers
{
    public class LoaiSpController : BaseController
    {
        private FreshEntities db = new FreshEntities();

        // GET: Admin/LoaiSp
        public ActionResult Index(string search, int page = 1, int pageSize = 10)
        {
            var dao = new SanPhamDao();
            var model = dao.ListAllPaging3(search, page, pageSize);
            ViewBag.Search = search; // giu thong tin tren khung tim kiem
            return View(model);
        }

        // GET: Admin/LoaiSp/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSp loaiSp = db.LoaiSp.Find(id);
            if (loaiSp == null)
            {
                return HttpNotFound();
            }
            return View(loaiSp);
        }

        // GET: Admin/LoaiSp/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLoai,TenLoai,Anh")] LoaiSp loaiSp)
        {
            if (ModelState.IsValid)
            {
                if (db.LoaiSp.Any(k => k.TenLoai == loaiSp.TenLoai))
                {
                    ViewBag.ThongBao = "Loại sản phẩm đã tồn tại";
                }
                else
                {
                    db.LoaiSp.Add(loaiSp);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(loaiSp);
        }

        // GET: Admin/LoaiSp/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSp loaiSp = db.LoaiSp.Find(id);
            if (loaiSp == null)
            {
                return HttpNotFound();
            }
            return View(loaiSp);
        }

        // POST: Admin/LoaiSp/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLoai,TenLoai,Anh")] LoaiSp loaiSp)
        {
            if (ModelState.IsValid)
            {
                if ((db.LoaiSp.Any(k => k.TenLoai == loaiSp.TenLoai)) && (db.LoaiSp.Any(k => k.MaLoai != loaiSp.MaLoai)))
                {
                    ViewBag.ThongBao = "Loại sản phẩm đã tồn tại";
                }
                else
                {
                    db.Entry(loaiSp).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(loaiSp);
        }

        // GET: Admin/LoaiSp/Delete/5
        public ActionResult Delete(int id)
        {
            var delE = db.LoaiSp.First(p => p.MaLoai == id);
            db.LoaiSp.Remove(delE);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
