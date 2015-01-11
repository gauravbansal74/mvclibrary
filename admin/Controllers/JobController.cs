using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLL;

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
            job objjob = objJobs.getJob(id);
            return View(objjob);
        }

        public JsonResult changejobStatus(Int64 jobId, int jobstatus, string reasonstatus)
        {
            Error objError = new Error();
            Jobs objJobs = new Jobs();
            objError = objJobs.changeJobStatus(jobId, jobstatus, reasonstatus, Convert.ToInt64(User.Identity.Name));
            return Json(objError, JsonRequestBehavior.AllowGet);
        }

    }
}
