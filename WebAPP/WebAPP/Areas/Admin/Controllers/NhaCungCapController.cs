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
    public class NhaCungCapController : BaseController
    {
        private FreshEntities db = new FreshEntities();

        // GET: Admin/NhaCungCap
        public ActionResult Index(string search, int page = 1, int pageSize = 10)
        {
            var dao = new SanPhamDao();
            var model = dao.ListAllPaging2(search, page, pageSize);
            ViewBag.Search = search; // giu thong tin tren khung tim kiem
            return View(model);
        }
        [HttpGet]
        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }
        // GET: Admin/NhaCungCap/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaCungCap nhaCungCap = db.NhaCungCap.Find(id);
            if (nhaCungCap == null)
            {
                return HttpNotFound();
            }
            return View(nhaCungCap);
        }


        // POST: Admin/NhaCungCap/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNCC,TenNCC,DiaChi")] NhaCungCap nhaCungCap)
        {
            if (ModelState.IsValid)
            {
                if (db.NhaCungCap.Any(k => k.TenNCC == nhaCungCap.TenNCC))
                {
                    ViewBag.ThongBao = "Nhà cung cấp đã tồn tại";
                }
                else
                {
                    db.NhaCungCap.Add(nhaCungCap);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(nhaCungCap);
        }

        // GET: Admin/NhaCungCap/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaCungCap nhaCungCap = db.NhaCungCap.Find(id);
            if (nhaCungCap == null)
            {
                return HttpNotFound();
            }
            return View(nhaCungCap);
        }

        // POST: Admin/NhaCungCap/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNCC,TenNCC,DiaChi")] NhaCungCap nhaCungCap)
        {
            if (ModelState.IsValid)
            {
                if ((db.NhaCungCap.Any(k => k.TenNCC == nhaCungCap.TenNCC))&&(db.NhaCungCap.Any(k => k.MaNCC != nhaCungCap.MaNCC)))
                {
                    ViewBag.ThongBao = "Nhà cung cấp đã tồn tại";
                }
                else
                {
                    db.Entry(nhaCungCap).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(nhaCungCap);
        }

        public ActionResult Delete(int id)
        {
            var delE = db.NhaCungCap.First(p => p.MaNCC == id);
            db.NhaCungCap.Remove(delE);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
