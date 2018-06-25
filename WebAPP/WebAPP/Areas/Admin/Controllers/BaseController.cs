
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebAPP.Common;

namespace WebAPP.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Admin/Base

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            var session = (UserLogin)Session[CommonConstants.USER_SECTION]; // kiem tra ss
            if (session == null || (Session["UserName"] == null))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new // null thi quay lai dang nhap
                {
                    controller = "Login",
                    action = "Index",
                    Area = "Admin"
                }
                )

                );
            }

            base.OnActionExecuted(filterContext);
        }
        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {

                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {

                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {

                TempData["AlertType"] = "alert-danger";
            }
        }
    }
}