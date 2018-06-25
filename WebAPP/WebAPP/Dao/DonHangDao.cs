using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPP.Models;
using PagedList;

namespace WebAPP.Dao
{
    public class DonHangDao
    {
        FreshEntities db = null;
        public DonHangDao()
        {
            db = new FreshEntities();
        }
        public long Insert(DonHang order)
        {
            db.DonHang.Add(order);
            db.SaveChanges();
            return order.MaDH;
        }

        public IEnumerable<DonHang> ListAllPaging(int page, int pageSize)
        {
            IQueryable<DonHang> model = db.DonHang;
            return model.OrderByDescending(x => x.NgayDat).ToPagedList(page, pageSize);
        }
        public IEnumerable<ChiTietDonHang> ListAllPaging2(int page, int pageSize)
        {
            IQueryable<ChiTietDonHang> model = db.ChiTietDonHang;
            return model.OrderByDescending(x => x.DonHang.NgayDat).ToPagedList(page, pageSize);
        }

    }
}