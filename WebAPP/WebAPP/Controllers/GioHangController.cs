using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using WebAPP.Models;
namespace WebAPP.Controllers
{
    public class GioHangController : Controller
    {
        FreshEntities db = new FreshEntities();
        // GET: GioHang
        public ActionResult Index()
        {
            List<GioHang> listGioHang = LayGioHang();//laygiohang luon tra ve 1 list cac giohangitem
            if (listGioHang.Count > 0)// giỏ hảng có sản phẩm nào
            {
                ViewBag.ThanhTien = TongTien().ToString();
            }
            Session["SoLuong"] = listGioHang.Sum(n => n.SoLuong);
            return View(listGioHang);
        }
        // Lấy giỏ hàng 
         public List<GioHang> LayGioHang()
        {
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang == null)// giỏ hàng chưa được tạo ra 
            {
                listGioHang = new List<GioHang>();//tạo 1 list giỏ hàng
                Session["GioHang"] = listGioHang;//sseion giohang lấy giỏ hàng hiện tại
                Session["SoLuong"] = 0;
            }
            return listGioHang;//trả về gio hang
        }
        //thêm vào giỏ hàng
        public ActionResult ThemGioHang(int maSp,string url)
        {
            // lấy ra giỏ hàng hiện tại, nếu chưa có th2i sẽ khởi tạo
            List<GioHang> listGioHang = LayGioHang();
            Session["SoLuong"] = listGioHang.Sum(n => n.SoLuong);
            // kiểm tra xem sản phẩm tồn tại trong list giỏ hàng hay chưa
            GioHang sanpham = listGioHang.Find(n => n.MaSP == maSp);
            if (sanpham == null)//nếu chưa tồn tại
            {
                sanpham = new GioHang(maSp);//tạo mới 1 sản phẩm trong giỏ hàng
                sanpham.SoLuong = 1;
                listGioHang.Add(sanpham);//thêm sản phẩm đó vào giỏ hàng
                Session["GioHang"] = listGioHang;
                Session["SoLuong"] = listGioHang.Sum(n => n.SoLuong);
                return Redirect(url);//trở về trang hiện tại
            }
            else //giỏ hàng đã có sản phẩm đó rồi
            {
                sanpham.SoLuong ++;
                Session["SoLuong"] = listGioHang.Sum(n => n.SoLuong);
                return Redirect(url);
            }

        }
        // tính tỗng tiền của giỏ hàng

        private Double TongTien()
        {
            Double TongTien = 0;
            // lấy giỏ hàng hiện tại được lưu torng session
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang != null)
            {
                //TongTien = listGioHang.Sum(n => n.ThanhTien);
                foreach (GioHang gh in listGioHang)
                {
                    TongTien += gh.ThanhTien;
                }
            }
            return TongTien;
        }
        public ActionResult XoaSanPham(int maSp)
        {

            List<GioHang> listGiohang = LayGioHang();
            GioHang tem = listGiohang.Find(x => x.MaSP == maSp);
            listGiohang.Remove(tem);
            Session["SoLuong"] = listGiohang.Sum(n => n.SoLuong);
            return RedirectToAction("Index");
           
        }
        // cập nhật giỏ hàng
        public ActionResult CapNhat( int maSP, FormCollection f, string url)
        {
            // lấy thông tin giỏ hàng hiện tại
            List<GioHang> listGioHang = LayGioHang();
            GioHang sp = listGioHang.SingleOrDefault(x => x.MaSP == maSP);
            if(sp != null)
            {
                sp.SoLuong = int.Parse(f["SoLuong"].ToString());// ep kieu
            }
            return Redirect(url);

        }
        public ActionResult XoaGioHang()
        {
            List<GioHang> listGioHang = LayGioHang();
            listGioHang.Clear();
            return RedirectToAction("Index");
        }
        // phương thức đặt hàng
       [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["ID"] == null)
                return RedirectToAction("Login", "HomeLayout");
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        public ActionResult DatHang(DonHang DH)
        {
            if (Session["ID"] == null)
                return RedirectToAction("Login", "HomeLayout");
            User khachhang = Session["TaiKhoan"] as User;
            List<GioHang> listGioHang = LayGioHang();
            ViewBag.ThanhTien = TongTien();

            if (DH.NgayGiao == null || DH.NgayGiao.ToString().Equals(""))
            {
                ViewBag.ngaygiao = "Chọn ngày bạn muốn nhận hàng";
            }
            if (DH.DiaChi == null || DH.DiaChi.Equals(""))
            {
                ViewBag.diachi = "Bạn phải nhập địa chỉ nhận hàng";
            }
            if (DH.Sdt == null || DH.Sdt.Equals(""))
            {
                ViewBag.sdt = "Bạn phải nhập số điện thoại người nhận hàng";
            }
            else if (Check.isSdt(DH.Sdt) == false)
                ViewBag.sdt = "Số diện thoại không đúng";
            else
            {
                // tạo mới 1 đối tượng đơn hàng dh
                DonHang dh = new DonHang();

                dh.MaKH = khachhang.MaKH;
                dh.TongTien = (long)TongTien();
                dh.NgayDat = DH.NgayDat;
                if (DH.NgayGiao == null)
                    dh.NgayGiao = DateTime.Now.AddDays(2);
                else
                    dh.NgayGiao = DH.NgayGiao;
                dh.TinhTrang = "Đang giao hàng";
                dh.DiaChi = DH.DiaChi;
                dh.Sdt = DH.Sdt;
                dh.LuuY = DH.LuuY;
                db.DonHang.Add(dh);
                db.SaveChanges();
                foreach (var item in listGioHang)
                {
                    ChiTietDonHang ctdh = new ChiTietDonHang();
                    ctdh.MaDH = dh.MaDH;
                    ctdh.MaSP = item.MaSP;
                    ctdh.SoLuong = item.SoLuong;
                    ctdh.DonGia = item.DonGia;
                    ctdh.Tongcong = (long)item.ThanhTien;
                    db.ChiTietDonHang.Add(ctdh);
                    db.SaveChanges();

                }
                Session["GioHang"] = null;
                Session["SoLuong"] = 0;
                return RedirectToAction("XacNhanDonHAng", "Giohang");
            }
            return View(DH);
        }
        public ActionResult XacNhanDonHang()
        {
            if (Session["ID"] == null)
                return RedirectToAction("Login", "HomeLayout");
            return View();
        }
    }
}