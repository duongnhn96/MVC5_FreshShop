using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebAPP
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}"); // ko cho ng dung nhap vao thu muc cua code
            routes.MapRoute(
               name: "LienHe",
               url: "lien-he",
               defaults: new { controller = "HomeLayout", action = "LienHe", id = UrlParameter.Optional }, namespaces: new[] { "WebAPP.Controllers" }
               );
            routes.MapRoute(
               name: "Gioithieu",
               url: "gioi-thieu",
               defaults: new { controller = "HomeLayout", action = "GioiThieu", id = UrlParameter.Optional }, namespaces: new[] { "WebAPP.Controllers" }
               );
            routes.MapRoute(
             name: "timkiem",
             url: "tim-kiem",
             defaults: new { controller = "TimKiem", action = "KetQuaTimKiem", id = UrlParameter.Optional }, namespaces: new[] { "WebAPP.Controllers" }
             );

            routes.MapRoute(
               name: "dangnhap",
               url: "dang-nhap",
               defaults: new { controller = "HomeLayout", action = "Login", id = UrlParameter.Optional }, namespaces: new[] { "WebAPP.Controllers" }
               );
            routes.MapRoute(
               name: "dangki",
               url: "dang-ki",
               defaults: new { controller = "HomeLayout", action = "Register", id = UrlParameter.Optional }, namespaces: new[] { "WebAPP.Controllers" }
               );
            routes.MapRoute(
                name: "histry",
                url: "lich-su-mua-hang",
                defaults: new { controller = "Login", action = "History", id = UrlParameter.Optional }, namespaces: new[] { "WebAPP.Controllers" }
                );
            routes.MapRoute(
                name: "setting",
                url: "cai-dat-tai-khoan",
                defaults: new { controller = "Login", action = "Setting", id = UrlParameter.Optional }, namespaces: new[] { "WebAPP.Controllers" }
                );
            routes.MapRoute( // phai de duoi cung
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "HomeLayout", action = "Index", id = UrlParameter.Optional }, namespaces: new[] { "WebAPP.Controllers" }
                );

           
        }

        }
    }

