using DLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace admin.Controllers
{
    public class EmailCampaignController : Controller
    {
        //
        // GET: /EmailCampaign/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult UnVerifiedUser(){
            Accounts objAccounts = new Accounts();
            List<account> objAccount = objAccounts.getUnVerifiedAccount();
            return View(objAccount);
        }

        public JsonResult sendEmailVerification(string email, string key)
        {
            Error objError = new Error();

            String path = Server.MapPath("~/emailtemplate/emailverify.html");
            string text = System.IO.File.ReadAllText(path);
            text = text.Replace("#useremail", email);
            text = text.Replace("#userverificationLink", ConfigurationManager.AppSettings["localurl"] + "Account/verifyemail/" + key);
            Task.Factory.StartNew(() =>
            {
                try
                {

                    MailMessage mail = new MailMessage();
                    mail.To.Add(new MailAddress(email));
                    mail.From = new MailAddress(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["adminemail"]));
                    mail.Subject = "Verify your email for Offcampus4u";
                    string Body = text;
                    mail.Body = Body;
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["adminsmtp"]);
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential
                    (Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["adminemail"]), Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["adminpassword"]));// Enter seders User name and password  
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
                catch
                {


                }
            });

            objError.isSuccess = true;
            objError.message = "We have created your account. please verify your email address.";
            return Json(objError, JsonRequestBehavior.AllowGet);
        }

    }
}
