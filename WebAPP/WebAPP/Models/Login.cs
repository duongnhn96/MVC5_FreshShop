using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPP.Models;
namespace WebAPP.Models
{
    public class LoginUser
    {
        FreshEntities db = new FreshEntities();
        private int MaKH { set; get; }
        private String TenKH { set; get; }
        //private string HinhAnh { set; get; }
        public LoginUser (User login)
        {
            MaKH = login.MaKH;
            TenKH = login.Hoten;
    }
    }
    
}