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
using PagedList.Mvc;
namespace WebAPP.Areas.Admin.Controllers
{
    public class DonHangController : BaseController
    {
        // GET: Admin/DonHang
        private FreshEntities db = new FreshEntities();

        // GET: DonHangs
        public ActionResult Index(int page=1, int pageSize = 11)
        {
            var dao = new DonHangDao();
            var model = dao.ListAllPaging(page, pageSize);

            return View(model);
        }
        //public ActionResult Index(int? page)
        //{
        //    int pageNum = (page ?? 1);

        //    return View(db.DonHang.OrderByDescending(x => x.NgayDat).ToPagedList(pageNum, 11));
        //}
        // GET: DonHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<ChiTietDonHang> list = db.ChiTietDonHang.Where(x => x.MaDH == id).ToList();
            if (list == null)
            {
                return RedirectToAction("Index");
            }
            DonHang donhang = db.DonHang.Find(id);
            donhang.TongTien= list.Sum(x => x.DonGia * x.SoLuong);
            db.SaveChanges();
            ViewBag.tongtien = list.Sum(x =>x.DonGia*x.SoLuong);
            return View(list);

        }

        // GET: DonHangs/Create
        public ActionResult Create()
        {
            ViewBag.MaKH = new SelectList(db.User, "MaKH", "Hoten");
            return View();
        }

        // POST: DonHangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDH,MaKH,TongTien,NgayDat,NgayGiao,LuuY,TinhTrang")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                db.DonHang.Add(donHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKH = new SelectList(db.User, "MaKH", "Hoten", donHang.MaKH);
            return View(donHang);
        }

        // GET: DonHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHang.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(db.User, "MaKH", "Hoten", donHang.MaKH);
            return View(donHang);
        }

        // POST: DonHangs/Edit/5
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDH,MaKH,TongTien,NgayDat,NgayGiao,LuuY,TinhTrang,Sdt,DiaChi")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donHang).State = EntityState.Modified;
                TongTienDH(donHang.MaDH);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKH = new SelectList(db.User, "MaKH", "Hoten", donHang.MaKH);
            return View(donHang);
        }
        public ActionResult Delete(int ? id)// xóa đơn hàng có mã DH = id

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHang.Find(id);
            List<ChiTietDonHang> list = db.ChiTietDonHang.Where(x => x.MaDH == id).ToList();
            foreach (ChiTietDonHang item in list)
            {
                db.ChiTietDonHang.Remove(item);
            }
            db.DonHang.Remove(donHang);

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public void TongTienDH(int ?maDH)
        {

            List<ChiTietDonHang> list = db.ChiTietDonHang.Where(x => x.MaDH == maDH).ToList();
            DonHang dh = db.DonHang.Where(x => x.MaDH == maDH).SingleOrDefault();
            dh.TongTien = list.Sum(x => x.SoLuong*x.DonGia);
            db.SaveChanges();
        }
    }
}