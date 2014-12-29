using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLL;
using mvclibrary.ViewModels;
using mvclibrary.customlib;
using DLL;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Mail;

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

        public ActionResult PostingHistory()
        {
            Jobs objJob = new DLL.Jobs();
            List<job> listApplyJob = objJob.getJobPostingHistory(Convert.ToInt64(User.Identity.Name));
            return View(listApplyJob);
        }

        public ActionResult AppliedJob()
        {
            Jobs objJob = new DLL.Jobs();
            List<applyJob> listApplyJob = objJob.getApplyHistory(Convert.ToInt64(User.Identity.Name));
            return View(listApplyJob);
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
            else
            {
                jobdata.jobOtherDetail = string.Empty;
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


        public ActionResult SearchJob(JobSearchViewModel jobSearch)
        {
            Jobs objJobs = new DLL.Jobs();
            List<job> listjob = objJobs.searchJob(jobSearch.skiilsdesignationcompany, jobSearch.location, jobSearch.jobMinExp, jobSearch.jobMinSalary);
            return View(listjob);
        }

        public ActionResult SearchJobCustom(string skiilsdesignationcompany, string location)
        {
            Jobs objJobs = new DLL.Jobs();
            List<job> listjob = objJobs.searchJobCustom(skiilsdesignationcompany, location);
            return View(listjob);
        }

        public JsonResult applyjobpost(string id)
        {

            Error objError = new DLL.Error();
            if (string.IsNullOrEmpty(id))
            {
                objError.isSuccess = false;
                objError.message = "Oops..Dont try to do useless things.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Jobs objJobs = new DLL.Jobs();
                objError = objJobs.applyJobPost(Convert.ToInt64(User.Identity.Name), Convert.ToInt64(id));
                if (objError.isSuccess)
                {
                    Profile objProfile = new Profile();
                    account objAccount = objProfile.GetPersonalInformation(Convert.ToInt64(User.Identity.Name));
                    string applicantemail = objAccount.email;
                    string applicantresume = objAccount.ResumeFileName;
                    string resumelink = Convert.ToString(ConfigurationManager.AppSettings["localurl"])+"UserFiles/Resume/"+applicantresume;
                    job objjob = objJobs.getJob(Convert.ToInt64(id));
                    string jobemail = objjob.jobApplyMode;
                    string jobpottitle = objjob.jobTitle;
                    String path = Server.MapPath("~/emailtemplate/jobapplication.html");
                    string text = System.IO.File.ReadAllText(path);
                    text = text.Replace("#applicantemail", applicantemail);
                    text = text.Replace("#resumelink", resumelink);
                    text = text.Replace("#useremail", jobemail);
                    text = text.Replace("#jobposttitle", jobpottitle);
                    Task.Factory.StartNew(() =>
                    {
                        try
                        {

                            MailMessage mail = new MailMessage();
                            mail.To.Add(new MailAddress(jobemail));
                            mail.From = new MailAddress(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["adminemail"]));
                            mail.Subject = "Application for your Job Post on Offcampus4u";
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
                        catch(Exception ex)
                        {


                        }
                    });
                }
                return Json(objError, JsonRequestBehavior.AllowGet);
            }
        }


    }
}
