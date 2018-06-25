using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebAPP.Models;
using PagedList;
using PagedList.Mvc;
using System.Net;

namespace WebAPP.Controllers
{
    public class HomeLayoutController : Controller
    {
        FreshEntities db = new FreshEntities();
        // GET: HomeLayout
        //public ActionResult Index(int ? page)
        //{
        //    // biế quy định số sp trên mỗi trang
        //    int pageSize = 6;
        //    // biến quy định số trang
        //    int pageNum = (page ?? 1);
        //    var sanPham = db.SanPham.Take(9);
        //    return View(sanPham.ToPagedList(pageNum, pageSize));
        //}
        public ActionResult Index()
        {
            
            return View(db.SanPham.Take(6).ToList());
        }
        public void ThongBao(String thongbao)
        {
            String strBuilder = "<script language='javascript'>alert('" + thongbao + "')</script>";
            Response.Write(strBuilder);

        }
        [HttpGet]
        public ActionResult Login()
        {
            // nếu có tk đăng nhập thì báo lỗi
            if (Session["TaiKhoan"] != null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return View();
        }

        [HttpPost]

        public ActionResult Login(User NguoiDangNhap)
        {
            // tìm user ligon torng csdl
            var check = db.User.FirstOrDefault(x => x.Username.Equals(NguoiDangNhap.Username));
            if (String.IsNullOrEmpty(NguoiDangNhap.Username))//kiểm ktra nhap ngoai view
                ViewData["LoiTaiKhoan"] = "Chưa nhập tài khoản";
            if (!String.IsNullOrEmpty(NguoiDangNhap.Username))
            {
                if (check == null)
                    ThongBao("Tai khoản không tồn tại");
                // nếu như tài khoản tồn tại thì kiểm tra mật khẩu
                else
                {
                    if (String.IsNullOrEmpty(NguoiDangNhap.MatKhau))
                        ViewData["LoiMatKhau"] = "Chưa nhập mật khẩu";

                    if (check.MatKhau.Equals(NguoiDangNhap.MatKhau))
                    {
                        String tenuser = NguoiDangNhap.Username.ToString();
                        //tenuser = Session["TenUser"] as String;
                        // gán session cho ng dang nhap
                        int ID = check.MaKH;
                        Session["UserName"] = tenuser;
                        Session["ID"] = ID;
                        Session["TaiKhoan"] = check;
                        return RedirectToAction("Index", "HomeLayout");//đăng nhập thành công
                    }

                    else
                    {
                        if (String.IsNullOrEmpty(NguoiDangNhap.MatKhau))
                            return View();
                        ThongBao("Mật khẩu không đúng");
                    }
                }
            }
            return View(NguoiDangNhap);
        }
        public ActionResult Register()
        {
            if(Session["TaiKhoan"] != null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View();
        }

        [HttpPost]
        public ActionResult Register( User user)
        {
            //[Bind(Include = "MaKH,Hoten,Username,MatKhau,Email,DienThoai,GioiTinh,NgaySinh,DiaChi")]
            if (ModelState.IsValid)
            {
                try
                {
                    if (db.User.Where(x => x.Username.ToLower() == user.Username.ToLower()).SingleOrDefault() != null)
                        ViewBag.thongbao = "Tài khoản đã tồn tại";
                    else
                    {
                        db.User.Add(user);
                        db.SaveChanges();

                        String tenuser = user.Username.ToString();
                        //tenuser = Session["TenUser"] as String;
                        int ID = user.MaKH;
                        Session["UserName"] = tenuser;
                        Session["ID"] = ID;
                        Session["TaiKhoan"] = user;
                        Session["ThongBao"] = null;
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception e)
                {

                }
            }

            return View(user);
        }
        // tạo view cho từng loại sản phẩm
        public ActionResult ProductViewType(int ? MaLoai)
        {
            List<SanPham> SanPham = (from sp in db.SanPham
                                     where sp.MaLoai == MaLoai
                                     select sp).ToList();
            ViewBag.TenLoai = db.LoaiSp.Find(MaLoai).TenLoai;
            if (SanPham.Count == 0)
            {
                ViewBag.ThongBaoSp = "Hiện Fresh Shop không còn sản phẩm "+ db.LoaiSp.Find(MaLoai).TenLoai+".  Cảm ơn sự ủng hộ của bạn! ";
            }
             else ViewBag.ThongBaoSP = "Bao gồm "+ SanPham.Count.ToString()+" sản phẩm";
            
            return View(SanPham);
        }
        public ActionResult GioiThieu()
        {

            return View();
        }

        public ActionResult lienHe()
        {

            return View();
        }
        public ActionResult Eror_404()
        {

            return View();
        }

    }
}