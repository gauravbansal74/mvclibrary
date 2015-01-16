using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace employer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HttpCookie wunCookie = new HttpCookie("WunelliCookie");
            bool remember = true;
            if (remember)
            {
                const int persistMonths = 12;
                wunCookie.Values.Add("username", "1");
                wunCookie.Expires = DateTime.Now.AddMonths(persistMonths);
            }
            else
            {
                wunCookie.Values.Add("username", "1");
                wunCookie.Expires = DateTime.Now.AddMinutes(5);
            }
            FormsAuthentication.SetAuthCookie("1", false);
            Response.Cookies.Add(wunCookie);
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return PartialView();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
