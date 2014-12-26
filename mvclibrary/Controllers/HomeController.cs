using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace mvclibrary.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            //MailMessage mail = new MailMessage();
            //mail.To.Add(new MailAddress("gauravbansal74@gmail.com"));
            //mail.From = new MailAddress("no-reply@offcampus4u.com");
            //mail.Subject = "Test Email from MVC4 App";
            //string Body = "Test Body from MVC4 App";
            //mail.Body = Body;
            //mail.IsBodyHtml = true;
            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = "smtp.gmail.com";
            //smtp.Port = 587;
            //smtp.UseDefaultCredentials = false;
            //smtp.Credentials = new System.Net.NetworkCredential
            //("glueplusdev@gmail.com", "Mxit1234");// Enter seders User name and password  
            //smtp.EnableSsl = true;
            //smtp.Send(mail);
            return View();
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
