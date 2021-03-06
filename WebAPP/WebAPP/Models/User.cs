﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAPP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.DonHang = new HashSet<DonHang>();
        }

        [Display(Name = "Mã Khách Hàng")]
        public int MaKH { get; set; }
        [Required(ErrorMessage = "Không được phép để trống"), StringLength(49, MinimumLength = 1, ErrorMessage = "Phải có ít nhất 1-100 kí tự")]
        [Display(Name = "Tên Khách Hàng")]
        public string Hoten { get; set; }
        [Display(Name = "Tên tài khoản")]
        [Required(ErrorMessage = "Không được phép để trống"), StringLength(49, MinimumLength = 1, ErrorMessage = "Phải có ít nhất 1-49 kí tự")]
        public string Username { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Không được phép để trống"), StringLength(49, MinimumLength = 1, ErrorMessage = "Phải có ít nhất 1-49 kí tự")]
        public string MatKhau { get; set; }
        [Required(ErrorMessage = "Không được phép để trống")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }
        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Không được phép để trống"), StringLength(11, MinimumLength = 10, ErrorMessage = "Số điện thoại phải có ít nhất 10 số")]
        [RegularExpression("^0[0-9]{9,10}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string DienThoai { get; set; }
        [RegularExpression("^(Nam|Nữ|nam|nữ|NAm|Nu|NaM|nu)$", ErrorMessage = "Giới tính chỉ có thể là Nam hoặc Nữ")]
        [Display(Name = "Giới tính")]
        public string GioiTinh { get; set; }
        [Display(Name = "Ngày sinh")]
        public Nullable<System.DateTime> NgaySinh { get; set; }
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonHang> DonHang { get; set; }
    }
}
