using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebAPP.Dao;
using WebAPP.Models;

namespace WebAPP.Areas.Admin.Controllers
{
    public class NguoiDungController : BaseController
    {
        private FreshEntities db = new FreshEntities();
        // GET: Admin/User
        public ActionResult Index(string search, int page = 1, int pageSize = 10)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(search, page, pageSize);
            ViewBag.Search = search; // giu thong tin tren khung tim kiem
            return View(model);
        }
        [HttpGet]
        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKH,Hoten,Username,MatKhau,Email,DienThoai,GioiTinh,NgaySinh,DiaChi")] User user)
        {
            if (ModelState.IsValid)
            {
                if (db.User.Any(k => k.Username == user.Username))
                {
                    ViewBag.ThongBao = "Người dùng đã tồn tại";
                }
                else
                {
                    try
                    {
                        db.User.Add(user);
                        db.SaveChanges();
                        return RedirectToAction("Index");

                    }
                    catch (Exception e)
                    {

                    }
                }
            }

            return View(user);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKH,Hoten,Username,MatKhau,Email,DienThoai,GioiTinh,NgaySinh,DiaChi")] User user)
        {
            if (ModelState.IsValid)
            {
                if ((db.User.Any(k => k.Username == user.Username))&& (db.User.Any(k => k.MaKH != user.MaKH) ) )
                {
                    ViewBag.ThongBao = "Người dùng đã tồn tại";
                }
                else
                {
                   
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    
                }    
             }
            
            return View(user);
        }

        public ActionResult Delete(int id)
        {
            var delE = db.User.First(p => p.MaKH == id);
            db.User.Remove(delE);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
