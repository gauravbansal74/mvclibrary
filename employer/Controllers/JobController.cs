using DLL;
using employer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace employer.Controllers
{
    public class JobController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult postHistory()
        {
            Jobs objJob = new DLL.Jobs();
            List<job> listApplyJob = objJob.getActiveJobPost(Convert.ToInt64(User.Identity.Name));
            return View(listApplyJob);
        }

        public ActionResult postUnHistory()
        {
            Jobs objJob = new DLL.Jobs();
            List<job> listApplyJob = objJob.getUnActiveJobPost(Convert.ToInt64(User.Identity.Name));
            return View(listApplyJob);
        }

        public JsonResult getJobHistory(){
            Jobs objJob = new DLL.Jobs();
            List<job> listApplyJob = objJob.getActiveJobPost(Convert.ToInt64(User.Identity.Name));
            return Json(listApplyJob,JsonRequestBehavior.AllowGet);
        }

        public ActionResult seeApplication(Int64 id)
        {
            Jobs objJob = new DLL.Jobs();
            List<applyJob> listApplyJob = objJob.getJobApplication(id);
            return View(listApplyJob);
        }

        public JsonResult getApplicatioList(Int64 id)
        {
            Jobs objJob = new DLL.Jobs();
            List<job> listApplyJob = objJob.getJobPostingHistory(Convert.ToInt64(User.Identity.Name));
            return Json(listApplyJob, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SaveJob()
        {
            return View();
        }
        

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
            else
            {
                jobdata.jobOtherDetail = string.Empty;
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
                EmpAccount objEmpAccount = new EmpAccount();
                company objCompany = objEmpAccount.getEmployerCompany(Convert.ToInt64(User.Identity.Name));
                Jobs objJob = new Jobs();
                job job = new job();
                job.jobCompanyInfo = objCompany.companyAbout;
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
                job.jobCompnayName = objCompany.companyName;
                job.jobTitle = jobdata.jobTitle;
                job.createdBy = Convert.ToInt64(User.Identity.Name);
                job.createdOn = DateTime.Now;
                job.modifiedBy = Convert.ToInt64(User.Identity.Name);
                job.modifiedOn = DateTime.Now;
                job.jobDeteled = false;
                job.jobStatus = 1;
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

        public JsonResult deleteJob(Int64 id)
        {
            Jobs objJobs = new Jobs();
            Error objError = objJobs.deleteJob(id, Convert.ToInt64(User.Identity.Name));
            return Json(objError, JsonRequestBehavior.AllowGet);
        }
        public JsonResult unpublishJob(Int64 id)
        {
            Jobs objJobs = new Jobs();
            Error objError = objJobs.unpublishJob(id, Convert.ToInt64(User.Identity.Name));
            return Json(objError, JsonRequestBehavior.AllowGet);
        }

    }
}
