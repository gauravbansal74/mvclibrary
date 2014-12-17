using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLL;
using System.Text.RegularExpressions;
using mvclibrary.customlib;
using System.Web.Security;

namespace mvclibrary.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            //if (User.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            //else
            //{
                return View();
            //}
        }

        [HttpPost]
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
                    return RedirectToAction("Index", "Course");
                }
                return RedirectToAction("Index", "Account");
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
                return Json(objError, JsonRequestBehavior.AllowGet);
            }
            else
            {
                objError.isSuccess = false;
                objError.message = "Password doesn't match with confirm password";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }
            
        }

    }
}
