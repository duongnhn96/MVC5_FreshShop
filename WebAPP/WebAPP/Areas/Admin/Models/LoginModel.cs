using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPP.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập tên tài khoản/email")]
        public string TaiKhoan { set; get; }
        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu")]
        public string MatKhau { set; get; }
        public bool RememberMe { set; get; }
    }
}