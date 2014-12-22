using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLL;
using mvclibrary.ViewModels;
using mvclibrary.customlib;
using DLL;

namespace mvclibrary.Controllers
{
    public class JobController : Controller
    {
        //
        // GET: /Job/
        private Jobs objJob;

        public ActionResult Index()
        {
           Jobs objJobs = new DLL.Jobs();
           List<job> listjob = objJobs.getJobs();
           return View(listjob);
        }

        [HttpGet]
        public ActionResult SaveJob()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]

        public JsonResult SaveJob(JobViewModel jobdata)
        {
            Error objError = new Error();
            if (string.IsNullOrEmpty(jobdata.jobTitle))
            {
                objError.isSuccess = false;
                objError.message = "Enter the Title.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }

            if (string.IsNullOrEmpty(jobdata.jobLocation))
            {
                objError.isSuccess = false;
                objError.message = "Enter the Location.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }

            if (string.IsNullOrEmpty(jobdata.jobCompnayName))
            {
                objError.isSuccess = false;
                objError.message = "Enter Name of the Company.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }

            if (jobdata.jobMinExp > jobdata.jobMaxExp)
            {
                objError.isSuccess = false;
                objError.message = "Min. Exp. cann't be greater than Max. Exp.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }

            if (jobdata.jobDisclosed)
            {
                if (jobdata.jobMinSalary > jobdata.jobMaxSalary)
                {
                    objError.isSuccess = false;
                    objError.message = "Min. Sal. cann't be greater than Max. Sal.";
                    return Json(objError, JsonRequestBehavior.AllowGet);
                }
            }

            if (string.IsNullOrEmpty(jobdata.jobDescription))
            {
                objError.isSuccess = false;
                objError.message = "Enter the Description.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }

            if (string.IsNullOrEmpty(jobdata.jobKeyword))
            {
                objError.isSuccess = false;
                objError.message = "Enter the Keywords.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }

            if (jobdata.jobOtherDetailPresent)
            {
                if (string.IsNullOrEmpty(jobdata.jobOtherDetail))
                {
                    objError.isSuccess = false;
                    objError.message = "Enter the Other Detail.";
                    return Json(objError, JsonRequestBehavior.AllowGet);
                }
            }

            if (string.IsNullOrEmpty(jobdata.jobCompanyInfo))
            {
                objError.isSuccess = false;
                objError.message = "Enter the Company Information.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }

            if (string.IsNullOrEmpty(jobdata.jobApplyMode))
            {
                objError.isSuccess = false;
                objError.message = "Enter the Source Email or Website Linkn.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }


            if (jobdata.jobApplyModeIsEmail)
            {
                RegexUtilities util = new RegexUtilities();
                if (!util.IsValidEmail(jobdata.jobApplyMode))
                {
                    objError.isSuccess = false;
                    objError.message = "Enter valid email address.";
                    return Json(objError, JsonRequestBehavior.AllowGet);
                }
            }

            if (jobdata.jobExpireDate != null)
            {
                objJob = new Jobs();
                job job = new job();
                job.jobCompanyInfo = jobdata.jobCompanyInfo;
                job.jobDescription = jobdata.jobDescription;
                job.jobDisclosed = jobdata.jobDisclosed;
                job.jobKeyword = jobdata.jobKeyword;
                job.jobMaxExp = jobdata.jobMaxExp;
                job.jobMaxSalary = jobdata.jobMaxSalary;
                job.jobMinExp = jobdata.jobMinExp;
                job.jobMinSalary = jobdata.jobMinSalary;
                job.jobOtherDetail = jobdata.jobOtherDetail;
                job.jobOtherDetailPresent = jobdata.jobOtherDetailPresent;
                job.jobApplyMode = jobdata.jobApplyMode;
                job.jobApplyModeIsEmail = jobdata.jobApplyModeIsEmail;
                job.jobExpireDate = jobdata.jobExpireDate;
                job.jobLocation = jobdata.jobLocation;
                job.jobCompnayName = jobdata.jobCompnayName;
                job.jobTitle = jobdata.jobTitle;
                job.createdBy = Convert.ToInt64(User.Identity.Name);
                job.createdOn = DateTime.Now;
                job.modifiedBy = Convert.ToInt64(User.Identity.Name);
                job.modifiedOn = DateTime.Now;
                job.jobDeteled = false;
                job.jobStatus = 0;
                objError = objJob.saveJob(job);
                return Json(objError, JsonRequestBehavior.AllowGet);
            }
            else
            {
                objError.isSuccess = false;
                objError.message = "Enter valid Expiry Date.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }
            

        }


        public ActionResult Detail(Int64 id)
        {
            Jobs objJobs = new DLL.Jobs();
            job objjob = objJobs.getJob(id);
            return View(objjob);
        }


    }
}
