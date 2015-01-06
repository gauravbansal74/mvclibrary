using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using employer.Filters;
using employer.Models;
using DLL;
using System.Configuration;
using System.Threading.Tasks;
using System.Net.Mail;

namespace employer.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult index()
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

        [HttpPost]
        [AllowAnonymous]
        public JsonResult createAccount(string phone, string name, string email, string password, string cfrmpassword)
        {
            Error objError = new Error();
            if (string.IsNullOrEmpty(name))
            {
                objError.isSuccess = false;
                objError.message = "Enter your name.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(phone))
            {
                objError.isSuccess = false;
                objError.message = "Enter the Phone Number with Extension.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }
            
            if (password.Length < 8 || cfrmpassword.Length < 8)
            {
                objError.isSuccess = false;
                objError.message = "Password length can't be less than 7 Characters.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }
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
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(cfrmpassword))
            {
                objError.isSuccess = false;
                objError.message = "Enter the password.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }

            if (password.Equals(cfrmpassword))
            {
                EmpAccount objEmpAccount = new EmpAccount();
                DLL.employer objEmployer = new DLL.employer();
                objEmployer.employerFirstName = name;
                objEmployer.employerPhoneNumber = phone;
                objEmployer.accountStatus = 0;
                objEmployer.createdOn = DateTime.Now;
                objEmployer.companyId = 1;
                objEmployer.employerDesignation = "Under Approval";
                objEmployer.employerEmail = email;
                objEmployer.employerPassword = password;
                objEmployer.isDeleted = false;
                objEmployer.modifiedBy = 1;
                objEmployer.modifiedOn = DateTime.Now;
                objError = objEmpAccount.registerAccount(objEmployer);
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
    }
}
