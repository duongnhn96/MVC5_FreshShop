using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAPP.Models;
using PagedList;
using PagedList.Mvc;
using System.Data.Entity;

namespace WebAPP.Controllers
{
    public class LoginController : CheckSessionController
    {
        FreshEntities db = new FreshEntities();
        // GET: Login
        public void ThongBao(String thongbao)
        {
            String strBuilder = "<script language='javascript'>alert('" + thongbao + "')</script>";
            Response.Write(strBuilder);

        }
        public ActionResult Index(int? page)
        {
            if (page == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["ID"] == null)
                return RedirectToAction("Login", "HomeLayout");
            int pageSize = 9;//số sản phẩm trên 1 trang
            int pageNumber = (page ?? 1);//nếu số sp k đủ 6 th2i số pag mặc định là 1
            return View(db.SanPham.OrderBy(n => n.MaSP).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Setting(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["ID"] == null)
                return RedirectToAction("Login", "HomeLayout");
            else
            {
                User user = db.User.Single(x => x.MaKH == id);
                if (user == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return View(user);
            }
        }
        [HttpPost]
        public ActionResult Setting(User user)
        {
            if (Session["ID"] != null)
            {
                int id = (int)Session["ID"];
                User capnhat = db.User.Find(id);
                try
                {
                    capnhat.MaKH = id;
                    capnhat.Hoten = user.Hoten;
                    capnhat.NgaySinh = user.NgaySinh;
                    capnhat.Email = user.Email;
                    capnhat.GioiTinh = user.GioiTinh;
                    capnhat.DiaChi = user.DiaChi;
                    capnhat.DienThoai = user.DienThoai;
                    db.SaveChanges();
                    ThongBao("Cập nhật thàng công");
                }
                catch (Exception exception)
                {

                }
                //}
                //return RedirectToAction("ThongtinUser", new { id = id});
                return View(user);
            }
            return View(user);
        }
        public ActionResult ThongtinUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["ID"] == null)
                return RedirectToAction("Login", "HomeLayout");
            User capnhat = db.User.Find(id);
            return View(capnhat);
        }
        public ActionResult LogOut()
        {
            if (Session["ID"] == null)
                return RedirectToAction("Login", "HomeLayout");
            Session["Username"] = null;
            Session["TaiKhoan"] = null;
            Session["ID"] = null;
            return RedirectToAction("Index", "HomeLayout");
        }
        // view hiễn thị lịch sử mua hàng của user đang đăng nhập
        public ActionResult History()
        {
            if (Session["ID"] == null)
                return RedirectToAction("Login", "HomeLayout");
            else
            {
                int ID = (int)Session["ID"];
                User login = db.User.Find(ID);
                List<DonHang> listDonHang = (from donhang in db.DonHang
                                             where donhang.MaKH == login.MaKH
                                             select donhang).ToList();
                if (listDonHang == null)
                {
                    ViewBag.ThongBao = "Bạn chưa có lượt mua hàng nào trên Fresh Shop";
                }
                else ViewBag.ThongBao = "Lịch sử mua hàng";
                foreach (DonHang item in listDonHang)
                {
                    if (item.NgayGiao < DateTime.Now)
                    {
                        item.TinhTrang = "Đã giao hàng";
                        db.SaveChanges();
                    }
                }
                return View(listDonHang);
            }
        }
        public ActionResult ChiTietDonHang(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["ID"] == null)
                return RedirectToAction("Login", "HomeLayout");
            else
            {
                List<ChiTietDonHang> listChiTiet = (from chitiet in db.ChiTietDonHang
                                                    where chitiet.MaDH == id
                                                    select chitiet).ToList();
                ViewBag.tongtien = listChiTiet.Sum(x => x.DonGia * x.SoLuong);
                return View(listChiTiet);
            }
        }
    }

}