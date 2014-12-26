using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLL;
using System.Text.RegularExpressions;
using mvclibrary.customlib;
using System.Web.Security;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Configuration;

namespace mvclibrary.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [AllowAnonymous]
        public ActionResult forgetpassword()
        {
            return View();
        }

        [AllowAnonymous]
        public JsonResult sendPassword(string email)
        {
            Error objError = new Error();
            Accounts objAccounts = new Accounts();
            string mypassword = objAccounts.getPasswordUsingEmail(email);
            String path = Server.MapPath("~/emailtemplate/forgotpassword.html");
            string text = System.IO.File.ReadAllText(path);
            text = text.Replace("#useremail", email);
            text = text.Replace("#userpassword", mypassword);
            Task.Factory.StartNew(() =>
            {
                try
                {

                    MailMessage mail = new MailMessage();
                    mail.To.Add(new MailAddress(email));
                    mail.From = new MailAddress(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["adminemail"]));
                    mail.Subject = "Password for Offcampus4u";
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
                    objError.isSuccess = true;
                    objError.message = "We have sent you password on your registred email address.";
                }
                catch
                {
                    objError.isSuccess = false;
                    objError.message = "OOps.. somthing went wrong. please try again after sometime.";
                }
            });
            objError.isSuccess = true;
            objError.message = "We have sent you password on your registred email address.";
            return Json(objError, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }

        [AllowAnonymous]
        public ActionResult checkLoginWithGmail(string email, string first_name, string last_name, string id, string birthday)
        {
            Error objError = new Error();
            if (string.IsNullOrEmpty(email))
            {
                objError.isSuccess = false;
                objError.message = "Enter the email address.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }

            if (string.IsNullOrEmpty(id))
            {
                objError.isSuccess = false;
                objError.message = "Google user id can't be null.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }
            RegexUtilities util = new RegexUtilities();
            if (!util.IsValidEmail(email))
            {
                objError.isSuccess = false;
                objError.message = "Enter valid email address.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Accounts objAccounts = new Accounts();
                objError = objAccounts.checkAccountWithGmail(email, first_name, last_name, id);
                if (objError.isSuccess)
                {
                    HttpCookie wunCookie = new HttpCookie("WunelliCookie");
                    bool remember = true;
                    if (remember)
                    {
                        const int persistMonths = 12;
                        wunCookie.Values.Add("username", objError.message);
                        wunCookie.Expires = DateTime.Now.AddMonths(persistMonths);
                    }
                    else
                    {
                        wunCookie.Values.Add("username", objError.message);
                        wunCookie.Expires = DateTime.Now.AddMinutes(5);
                    }
                    FormsAuthentication.SetAuthCookie(objError.message, false);
                    Response.Cookies.Add(wunCookie);
                    objError.isSuccess = true;
                    objError.message = "Successfully Logged in.";
                    return Json(objError, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    objError.isSuccess = false;
                    objError.message = "Email or password is wrong.";
                    return Json(objError, JsonRequestBehavior.AllowGet);
                }
            }
        }
       
        [AllowAnonymous]
        public ActionResult checkLogin(string email, string password)
        {
            Error objError = new Error();
            if (string.IsNullOrEmpty(email))
            {
                objError.isSuccess = false;
                objError.message = "Enter the email address.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }
            RegexUtilities util = new RegexUtilities();
            if (!util.IsValidEmail(email))
            {
                objError.isSuccess = false;
                objError.message = "Enter valid email address.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(password))
            {
                objError.isSuccess = false;
                objError.message = "Enter the password.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Accounts objAccounts = new Accounts();
                objError = objAccounts.checkAccount(email, password);
                if (objError.isSuccess)
                {
                    HttpCookie wunCookie = new HttpCookie("WunelliCookie");
                    bool remember = true;
                    if (remember)
                    {
                        const int persistMonths = 12;
                        wunCookie.Values.Add("username", objError.message);
                        wunCookie.Expires = DateTime.Now.AddMonths(persistMonths);
                    }
                    else
                    {
                        wunCookie.Values.Add("username", objError.message);
                        wunCookie.Expires = DateTime.Now.AddMinutes(5);
                    }
                    FormsAuthentication.SetAuthCookie(objError.message, false);
                    Response.Cookies.Add(wunCookie);
                    objError.isSuccess = true;
                    objError.message = "Successfully Logged in.";
                    return Json(objError, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    objError.isSuccess = false;
                    objError.message = "Email or password is wrong.";
                    return Json(objError, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult createAccount(string email, string password, string cfrmpassword)
        {
            Error objError = new Error();
            if (string.IsNullOrEmpty(email))
            {
                objError.isSuccess = false;
                objError.message = "Enter the email address.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }
            RegexUtilities util = new RegexUtilities();
            if (!util.IsValidEmail(email))
            {
                objError.isSuccess = false;
                objError.message = "Enter valid email address.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }
            if(string.IsNullOrEmpty(password) || string.IsNullOrEmpty(cfrmpassword)){
                objError.isSuccess = false;
                objError.message = "Enter the password.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }

            if (password.Equals(cfrmpassword))
            {
                Accounts objAccounts = new Accounts();
                account objAccount = new account();
                objAccount.accountStatus = 0;
                objAccount.createdOn = DateTime.Now;
                objAccount.email = email;
                objAccount.password = password;
                objAccount.isDeleted = false;
                objAccount.modifiedBy = 1;
                objAccount.modifiedOn = DateTime.Now;
                objError = objAccounts.registerAccount(objAccount);
                if (objError.isSuccess)
                {
                    String path = Server.MapPath("~/emailtemplate/emailverify.html");
                    string text = System.IO.File.ReadAllText(path);
                    text = text.Replace("#useremail", email);
                    text = text.Replace("#userverificationLink", ConfigurationManager.AppSettings["localurl"] + "Account/verifyemail/" + objError.message);
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
                else
                {
                    return Json(objError, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                objError.isSuccess = false;
                objError.message = "Password doesn't match with confirm password";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }
            
        }


        [AllowAnonymous]
        public ActionResult verifyemail(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Accounts objAccounts = new Accounts();
                Error objError = objAccounts.verifyEmail(id);
                if (objError.isSuccess)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("verifyemailerror");
                }
            }
            else
            {
                return RedirectToAction("verifyemailerror");
            }
        }

        [AllowAnonymous]
        public ActionResult verifyemailerror()
        {

            return View();
        }
    }
}
