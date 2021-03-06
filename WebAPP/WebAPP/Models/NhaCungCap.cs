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

    public partial class NhaCungCap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhaCungCap()
        {
            this.SanPham = new HashSet<SanPham>();
        }
        [Required(ErrorMessage = "Không được phép để trống")]
        [Display(Name = "Mã nhà cung cấp")]
        public int MaNCC { get; set; }
        [Display(Name = "Tên nhà cung cấp")]
        [Required(ErrorMessage = "Không được phép để trống"), StringLength(49, MinimumLength = 1, ErrorMessage = "Phải có ít nhất 1-49 kí tự")]
        public string TenNCC { get; set; }
        [Display(Name = "Địa chỉ nhà cung cấp")]
        [Required(ErrorMessage = "Không được phép để trống")]
        public string DiaChi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [Required(ErrorMessage = "Không được phép để trống")]
        public virtual ICollection<SanPham> SanPham { get; set; }
    }
}
