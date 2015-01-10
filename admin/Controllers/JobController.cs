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

    }
}
