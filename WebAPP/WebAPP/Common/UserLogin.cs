using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPP.Common
{
    [Serializable]
    public class UserLogin
    {
        public long UserID { set; get; }
        public string TaiKhoan { set; get; }

    }
}