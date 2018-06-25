using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAPP.Areas.Admin.Models;
using WebAPP.Common;
using WebAPP.Dao;
using WebAPP.Models;
namespace WebAPP.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        FreshEntities db = new FreshEntities();
        // GET: Admin/Login
        public ActionResult Index()
        {
           
            return View();
        }
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User login = db.User.Where(x => x.Username == model.TaiKhoan && x.MatKhau == model.MatKhau).SingleOrDefault();
                if (login != null)
                {
                    if (login.Username.Equals("admin"))
                    {

                        var userSession = new UserLogin();//tạo session
                        userSession.UserID = login.MaKH;
                        Session.Add(CommonConstants.USER_SECTION, userSession);
                        Session["ID"] = login.MaKH;
                        Session["TaiKhoan"] = login;
                        Session["UserName"] = login.Hoten;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Tài khoản không được phép truy cập vào admin!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tên tài khoản hoặc mật khẩu chưa đúng!");
                }

            }
            return View("Index");

        }
        public ActionResult Logout()
        {
            if (Session["ID"] == null)
                return RedirectToAction("Index");
            CommonConstants.USER_SECTION = null;
            Session["Username"] = null;
            Session["TaiKhoan"] = null;
            Session["ID"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}