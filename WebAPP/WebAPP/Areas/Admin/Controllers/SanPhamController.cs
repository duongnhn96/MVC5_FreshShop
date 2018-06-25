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
    public class SanPhamController : BaseController
    {
        // GET: Admin/SanPham
        private FreshEntities db = new FreshEntities();

        // GET: SanPhams
        public ActionResult Index(string search, int page = 1, int pageSize = 11)
        {
            var dao = new SanPhamDao();
            var model = dao.ListAllPaging(search, page, pageSize);
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MaLoai = new SelectList(db.LoaiSp, "MaLoai", "TenLoai");
            ViewBag.NhaCungCap = new SelectList(db.NhaCungCap, "MaNCC", "TenNCC");
            return View();
        }


        [HttpPost, ValidateInput(false)] // nhap vao duoc html
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,GiaBan,MoTa,HinhAnh,NgayCapNhat,SoLuongCon,MaLoai,NhaCungCap,HanSuDung")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                if (db.SanPham.Any(k => k.TenSP == sanPham.TenSP))
                {
                    ViewBag.ThongBao = "Sản phẩm đã tồn tại";
                }
                else
                {
                    sanPham.NgayCapNhat = DateTime.Today;
                    db.SanPham.Add(sanPham);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,GiaBan,MoTa,NgayCapNhat,SoLuongCon,MaLoai,NhaCungCap,HanSuDung")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                if ((db.SanPham.Any(k => k.TenSP == sanPham.TenSP)) && (db.SanPham.Any(k => k.MaSP != sanPham.MaSP)))
                {
                    ViewBag.ThongBao = "Sản phẩm đã tồn tại";
                }
                else
                {
                    db.Entry(sanPham).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.MaLoai = new SelectList(db.LoaiSp, "MaLoai", "TenLoai", sanPham.MaLoai);
            ViewBag.NhaCungCap = new SelectList(db.NhaCungCap, "MaNCC", "TenNCC", sanPham.NhaCungCap);
            return View(sanPham);
        }

        public ActionResult Delete(int id)
        {
            var del = db.SanPham.First(p => p.MaSP==id);
            db.SanPham.Remove(del);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}