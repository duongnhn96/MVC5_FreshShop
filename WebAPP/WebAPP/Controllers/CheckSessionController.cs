using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebAPP.Controllers
{
    public class CheckSessionController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            var session = Session["TaiKhoan"];
            if (session == null)
            {
                Session["ThongBao"] = "Bạn phải đăng nhập đề xem nội dung trang";
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new // null thi quay lai dang nhap
                {
                    controller = "HomeLayout",
                    action = "Login",
                    Area = ""
                }
                )

                );
            }

            base.OnActionExecuted(filterContext);
        }
        // GET: CheckSession
        public ActionResult Index()
        {
            return View();
        }
    }
}