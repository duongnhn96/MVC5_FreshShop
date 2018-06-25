using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPP.Models;

namespace WebAPP.Models
{
    [Serializable]
    public class GioHang
    {
        FreshEntities db = new FreshEntities();
        public int MaSP;
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public string HinhAnh { get; set; }
        public Double ThanhTien
        {
            get
            {
                return SoLuong * DonGia;

            }
        }
        public long DonGia { get; set; }
        // tạo 1 đối tượng giỏ hàng
        public GioHang(int maSp)
        {
            MaSP = maSp;
            SanPham sp = db.SanPham.Single(x => x.MaSP == maSp);
            HinhAnh = sp.HinhAnh;
            TenSP = sp.TenSP;
            DonGia = (long)sp.GiaBan;  //long.Parse(sp.GiaBan.GetValueOrDefault(0).ToString());
            SoLuong = 0;
        }
    }
}