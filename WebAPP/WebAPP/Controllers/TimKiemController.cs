using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAPP.Models;
using PagedList;
using PagedList.Mvc;

namespace WebAPP.Controllers
{
    public class TimKiemController : Controller
    {
        FreshEntities db = new FreshEntities();
        // GET: TimKiem
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KetQuaTimKiem(int ? page,FormCollection f)
        {
            string TuKhoa = f["TuKhoa"];
            ViewBag.tukhoa = TuKhoa;
            // tìm kiếm theo tên sản phẩm
            List<SanPham> list = db.SanPham.Where(n => n.TenSP.Contains(TuKhoa)).ToList();
            // phân trang
            int pageSize = 9;
            int pageNumber = (page ?? 1);//nếu ít hơn 9sp thì mặc định 1 trang
            
            if(list.Count > 0)
            {
                ViewBag.thongbao = "Đã tìm thấy " + list.Count.ToString() + " sản phẩm";
                return View(list.OrderBy(x => x.MaLoai).ToPagedList(pageNumber, pageSize));
            }
            else {
                ViewBag.thongbao = "Không tìm thấy sản phẩm nào. Bạn có thể tham khảo các sản phẩm dưới đây";

                return View(db.SanPham.OrderByDescending(x => x.NgayCapNhat).Take(6).ToPagedList(pageNumber, pageSize));
            }
        }
        [HttpGet]
        public ActionResult KetQuaTimKiem(int? page,String TuKhoa)
        {
            
            ViewBag.tukhoa = TuKhoa;
            List<SanPham> list = db.SanPham.Where(n => n.TenSP.Contains(TuKhoa)).ToList();
            // phân trang
            int pageSize = 9;
            int pageNumber = (page ?? 1);

            if (list.Count > 0)
            {
                ViewBag.thongbao = "Đã tìm thấy " + list.Count.ToString() + " sản phẩm";
                return View(list.OrderBy(x => x.MaLoai).ToPagedList(pageNumber, pageSize));
            }
            else
            {
                ViewBag.thongbao = "Không tìm thấy sản phẩm nào. Bạn có thể tham khảo các sản phẩm dưới đây";

                return View(db.SanPham.OrderByDescending(x => x.NgayCapNhat).Take(6).ToPagedList(pageNumber, pageSize));
            }
        }
    }
}