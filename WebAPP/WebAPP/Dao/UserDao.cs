using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPP.Models;

namespace WebAPP.Dao
{
    public class UserDao
    {

        FreshEntities db = new FreshEntities();
        public UserDao()
        {
            db = new FreshEntities();
        }
        public int Insert(User entity)
        {
            db.User.Add(entity);
            db.SaveChanges();
            return entity.MaKH;
        }
  
        public User GetById(string taiKhoan)
        {
            return db.User.SingleOrDefault(x => x.Username == taiKhoan);
        }
        public bool Login(string TaiKhoan, string MatKhau)
        {
            var result = db.User.Count(x => x.Username == TaiKhoan && x.MatKhau == MatKhau);
            if (result > 0)
            {
                return true;

            }
            else return false;
        }
        public IEnumerable<User> ListAllPaging(string search, int page, int pageSize)
        {
            IQueryable<User> model = db.User;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.Username.Contains(search) || x.Username.Contains(search));
            }

            return model.OrderByDescending(x => x.MaKH).ToPagedList(page, pageSize);
        }
        public IEnumerable<User> ListAllPaging(int page, int pageSize)
        {
            IQueryable<User> model = db.User;
            return model.OrderByDescending(x => x.Hoten).ToPagedList(page, pageSize);
        }
       

    }
}