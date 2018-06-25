using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPP.Models;

namespace WebAPP.Dao
{
    public class SanPhamDao
    {
        FreshEntities db = null;
        public static string USER_SESSION = "USER_SESSION";
        public SanPhamDao()
        {
            db = new FreshEntities();
        }
        public IEnumerable<SanPham> ListAllPaging(string search, int page, int pageSize)
        {
            IQueryable<SanPham> model = db.SanPham;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.TenSP.Contains(search) || x.TenSP.Contains(search));
            }

            return model.OrderByDescending(x => x.MaSP).ToPagedList(page, pageSize);
        }
        public IEnumerable<SanPham> ListAllPaging(int page, int pageSize)
        {
            IQueryable<SanPham> model = db.SanPham;
            return model.OrderByDescending(x => x.MaSP).ToPagedList(page, pageSize);
        }
        public SanPham GetByID(int id)
        {

            return db.SanPham.Find(id);
        }

        // phan trang, tim kiem nha cc
        public IEnumerable<NhaCungCap> ListAllPaging2(string search, int page, int pageSize)
        {
            IQueryable<NhaCungCap> model = db.NhaCungCap;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.TenNCC.Contains(search) || x.TenNCC.Contains(search));
            }

            return model.OrderByDescending(x => x.MaNCC).ToPagedList(page, pageSize);
        }
        public IEnumerable<NhaCungCap> ListAllPaging2(int page, int pageSize)
        {
            IQueryable<NhaCungCap> model = db.NhaCungCap;
            return model.OrderByDescending(x => x.TenNCC).ToPagedList(page, pageSize);
        }

        // phan trang tim kiem loai san pham
        public IEnumerable<LoaiSp> ListAllPaging3(string search, int page, int pageSize)
        {
            IQueryable<LoaiSp> model = db.LoaiSp;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.TenLoai.Contains(search) || x.TenLoai.Contains(search));
            }

            return model.OrderByDescending(x => x.MaLoai).ToPagedList(page, pageSize);
        }
        public IEnumerable<LoaiSp> ListAllPaging3(int page, int pageSize)
        {
            IQueryable<LoaiSp> model = db.LoaiSp;
            return model.OrderByDescending(x => x.MaLoai).ToPagedList(page, pageSize);
        }

    }
} 