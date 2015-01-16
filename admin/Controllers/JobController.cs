using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLL;
using DLL.ViewModel;

namespace admin.Controllers
{
    public class JobController : Controller
    {
        //
        // GET: /Job/

        public ActionResult Index()
        {
            AdminOff objAdmin = new AdminOff();
            List<job> objJobList = objAdmin.getunApproveJobs();
            return View(objJobList);
        }

        public ActionResult seeJob(Int64 id)
        {
            Jobs objJobs = new DLL.Jobs();
            JobDLLViewModel objjob = objJobs.getJob(id);
            return View(objjob);
        }

        public JsonResult changejobStatus(Int64 jobId, int jobstatus, string reasonstatus)
        {
            Error objError = new Error();
            objError.isSuccess = false;
            objError.message = "Oops.. something went wrong. please try again later.";
            Jobs objJobs = new Jobs();
            objError = objJobs.changeJobStatus(jobId, jobstatus, reasonstatus, Convert.ToInt64(User.Identity.Name));
            if (objError.isSuccess && jobstatus.Equals(1))
            {
                EmailNotifier objEmailNotifier = new EmailNotifier();
                Accounts objAccounts = new Accounts();
                account objAccount = objAccounts.getAccount(Convert.ToInt64(objError.message));
                MyWallet objMyWallet = new MyWallet();
                wallet objwallet = objMyWallet.getmyWallet(Convert.ToInt64(objError.message));
               // Error objError2 = objEmailNotifier.getUserEmail(Convert.ToInt64(User.Identity.Name));
                if (objAccount != null && objwallet != null)
                {
                    String path = Server.MapPath("~/emailtemplate/postapproved.html");
                    string text = System.IO.File.ReadAllText(path);
                    text = text.Replace("#useremail", objAccount.email);
                    text = text.Replace("#earning", objwallet.walletBalance);
                    objError = objEmailNotifier.sendEmail(objAccount.email, text, "Offcampus4u : Congratulations Your Post Approved and Wallet Information is....");
                }
                objError.isSuccess = true;
                objError.message = "Success.";
            }

            if (objError.isSuccess && jobstatus.Equals(2))
            {
                EmailNotifier objEmailNotifier = new EmailNotifier();
                Accounts objAccounts = new Accounts();
                account objAccount = objAccounts.getAccount(Convert.ToInt64(objError.message));
                MyWallet objMyWallet = new MyWallet();
                wallet objwallet = objMyWallet.getmyWallet(Convert.ToInt64(objError.message));
                // Error objError2 = objEmailNotifier.getUserEmail(Convert.ToInt64(User.Identity.Name));
                if (objAccount != null && objwallet != null)
                {
                    String path = Server.MapPath("~/emailtemplate/postrejected.html");
                    string text = System.IO.File.ReadAllText(path);
                    text = text.Replace("#useremail", objAccount.email);
                    text = text.Replace("#earning", objwallet.walletBalance);
                    objError = objEmailNotifier.sendEmail(objAccount.email, text, "Offcampus4u : Your Post Rejected and Wallet Information is....");
                }
                objError.isSuccess = true;
                objError.message = "Success.";
            }
            return Json(objError, JsonRequestBehavior.AllowGet);
        }

    }
}
