using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLL;
using mvclibrary.ViewModels;


namespace mvclibrary.Controllers
{
    public class JobController : Controller
    {
        //
        // GET: /Job/
        private Jobs objJob;

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SaveJob()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveJob(JobViewModel job1)
        {
            if (ModelState.IsValid)
            {
                objJob = new Jobs();
                job job = new job();
                job.jobCompanyInfo = job1.jobCompanyInfo;
                job.jobDescription = job1.jobDescription;
                job.jobDisclosed = job.jobDisclosed;
                job.jobKeyword = job1.jobKeyword;
                job.jobMaxExp = job1.jobMaxExp;
                job.jobMaxSalary = job1.jobMaxSalary;
                job.jobMinExp = job1.jobMinExp;
                job.jobMinSalary = job1.jobMinSalary;
                job.jobOtherDetail = job1.jobOtherDetail;
                job.jobOtherDetailPresent = job1.jobOtherDetailPresent;
                job.jobTitle = job1.jobTitle;
                job.createdBy = 1;
                job.createdOn = DateTime.Now;
                job.modifiedBy = 1;
                job.modifiedOn = DateTime.Now;
                job.jobDeteled = false;
                job.jobStatus = 0;
                objJob.saveJob(job);
                return Content("Error in Form");
            }
            else
            {

                return View(job1);
            }

        }


    }
}
