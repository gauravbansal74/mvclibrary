
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DLL
{
   public class Jobs
    {
       private offcampus4uEntities db;

       public List<job> getJobs()
       {
           List<job> listJob = new List<job>();
           db = new offcampus4uEntities();
           listJob = db.jobs.Where(x => x.jobDeteled.Equals(false) && x.jobStatus.Equals(1)).OrderByDescending(x=>x.jobId).ToList<job>();
           return listJob;
       }

       public List<job> getunApproveJobs()
       {
           List<job> listJob = new List<job>();
           db = new offcampus4uEntities();
           listJob = db.jobs.Where(x => x.jobDeteled.Equals(false) && x.jobStatus.Equals(0)).ToList<job>();
           return listJob;
       }

       public job getJob(Int64 jobId)
       {
           db = new offcampus4uEntities();
           job objJob = db.jobs.Where(x => x.jobId.Equals(jobId) && x.jobDeteled.Equals(false)).FirstOrDefault();
           return objJob;
       }

       public List<job> searchJob(string skillsdesignationcompany, string location, int experience, int salary)
       {
           List<job> listJob = new List<job>();
           db = new offcampus4uEntities();
           if(!string.IsNullOrWhiteSpace(skillsdesignationcompany) && !string.IsNullOrEmpty(location)){
               listJob = db.jobs.Where(x => x.jobCompnayName.Contains(skillsdesignationcompany) || x.jobDescription.Contains(skillsdesignationcompany) || x.jobTitle.Contains(skillsdesignationcompany) || x.jobLocation.Contains(location) || x.jobMinExp <= experience || x.jobMaxExp >= experience || x.jobMinSalary <= salary || x.jobMaxSalary >= salary).OrderByDescending(x => x.jobId).ToList<job>();
           }

           if (string.IsNullOrEmpty(skillsdesignationcompany) && !string.IsNullOrEmpty(location))
           {
               listJob = db.jobs.Where(x => x.jobLocation.Contains(location) || x.jobMinExp <= experience || x.jobMaxExp >= experience || x.jobMinSalary <= salary || x.jobMaxSalary >= salary).OrderByDescending(x => x.jobId).ToList<job>();
           }

           if (string.IsNullOrEmpty(location) && !string.IsNullOrEmpty(skillsdesignationcompany))
           {
               listJob = db.jobs.Where(x => x.jobCompnayName.Contains(skillsdesignationcompany) || x.jobDescription.Contains(skillsdesignationcompany) || x.jobTitle.Contains(skillsdesignationcompany) || x.jobLocation.Contains(location) || x.jobMinExp <= experience || x.jobMaxExp >= experience || x.jobMinSalary <= salary || x.jobMaxSalary >= salary).OrderByDescending(x => x.jobId).ToList<job>();
           }
         //  listJob = db.jobs.Where(x => x.jobDeteled.Equals(false)).ToList<job>();
           return listJob;
       }

       public List<job> searchJobCustom(string skillsdesignationcompany, string location)
       {
           List<job> listJob = new List<job>();
           db = new offcampus4uEntities();
           if (!string.IsNullOrWhiteSpace(skillsdesignationcompany) && !string.IsNullOrEmpty(location))
           {
               listJob = db.jobs.Where(x => x.jobCompnayName.Contains(skillsdesignationcompany) || x.jobDescription.Contains(skillsdesignationcompany) || x.jobTitle.Contains(skillsdesignationcompany) || x.jobLocation.Contains(location)).OrderByDescending(x => x.jobId).ToList<job>();
           }

           if (string.IsNullOrEmpty(skillsdesignationcompany) && !string.IsNullOrEmpty(location))
           {
               listJob = db.jobs.Where(x => x.jobLocation.Contains(location)).OrderByDescending(x => x.jobId).ToList<job>();
           }

           if (string.IsNullOrEmpty(location) && !string.IsNullOrEmpty(skillsdesignationcompany))
           {
               listJob = db.jobs.Where(x => x.jobCompnayName.Contains(skillsdesignationcompany) || x.jobDescription.Contains(skillsdesignationcompany) || x.jobTitle.Contains(skillsdesignationcompany) || x.jobLocation.Contains(location)).OrderByDescending(x => x.jobId).ToList<job>();
           }
           //  listJob = db.jobs.Where(x => x.jobDeteled.Equals(false)).ToList<job>();
           return listJob;
       }

       public Error saveJob(job job){
           Error objError = new Error();
           try
           {
               db = new offcampus4uEntities();
               db.jobs.Add(job);
               db.SaveChanges();
               objError.isSuccess = true;
               objError.message = "Successfully updated";
               return objError;
           }
           catch(Exception ex)
           {
               objError.isSuccess = false;
               objError.message = "Oops.. Somthing went wrong. please try again after sometime.";
               return objError;
           }
       }

       public Error applyJobPost(Int64 userid, Int64 jobid)
       {
           Error objError = new Error();
           try
           {
               db = new offcampus4uEntities();
               applyJob objApplyJob = db.applyJobs.Where(x => x.accountId.Equals(userid) && x.jobId.Equals(jobid)).FirstOrDefault();
               if (objApplyJob != null)
               {
                   objError.isSuccess = false;
                   objError.message = "You have already applied for this job.";
               }
               else
               {
                   applyJob objApplyjob1 = new applyJob();
                   objApplyjob1.accountId = userid;
                   objApplyjob1.jobId = jobid;
                   objApplyjob1.createdBy = userid;
                   objApplyjob1.createdOn = DateTime.Now;
                   objApplyjob1.modifiedBy = userid;
                   objApplyjob1.modifiedOn = DateTime.Now;
                   objApplyjob1.isDeleted = false;
                   db.applyJobs.Add(objApplyjob1);
                   db.SaveChanges();
                   objError.isSuccess = true;
                   objError.message = "You have successfully applied for this job.";
               }
           }
           catch (Exception ex)
           {
               objError.isSuccess = false;
               objError.message = "Oops. something went wrong. please try again after sometime.";
           }
           return objError;
       }

       public List<applyJob> getApplyHistory(Int64 id)
       {
           db = new offcampus4uEntities();
           List<applyJob> objApplyHistory = db.applyJobs.Where(x => x.accountId.Equals(id)).OrderByDescending(x => x.jobId).Take(10).ToList<applyJob>();
           return objApplyHistory;
       }

       public List<job> getJobPostingHistory(Int64 id)
       {
           db = new offcampus4uEntities();
           List<job> objApplyHistory = db.jobs.Where(x => x.createdBy.Equals(id)).OrderByDescending(x => x.jobId).Take(10).ToList<job>();
           return objApplyHistory;
       }

       public Error changeJobStatus(Int64 jobId, int status, Int64 accountId)
       {
           Error objError = new Error();
           
           using(var transaction = new TransactionScope()){
               try
               {
                   db = new offcampus4uEntities();
                   job objJob = db.jobs.Where(x => x.jobId.Equals(jobId)).FirstOrDefault();
                   if (objJob != null)
                   {
                       objJob.jobStatus = status;
                       objJob.modifiedBy = accountId;
                       objJob.modifiedOn = DateTime.Now;
                       db.SaveChanges();
                       if (status == 1)
                       {
                           wallet objWallet = db.wallets.Where(x => x.accountId.Equals(objJob.createdBy)).FirstOrDefault();
                           if (objWallet != null)
                           {
                               walletTransaction objwalletTransaction = new walletTransaction();
                               objwalletTransaction.transactionAmount = Convert.ToString(ConfigurationManager.AppSettings["amountforpost"]);
                               objwalletTransaction.walletId = objWallet.walletId;
                               objwalletTransaction.walletDescription = "Job post successfully approved";
                               objwalletTransaction.createdBy = accountId;
                               objwalletTransaction.createdOn = DateTime.Now;
                               objwalletTransaction.modifiedBy = accountId;
                               objwalletTransaction.modifiedOn = DateTime.Now;
                               objwalletTransaction.transactionStatus = 1;
                               objwalletTransaction.isDeleted = false;
                               db.walletTransactions.Add(objwalletTransaction);
                               objWallet.walletBalance = Convert.ToString(Convert.ToInt64(objWallet.walletBalance) + Convert.ToInt64(ConfigurationManager.AppSettings["amountforpost"]));
                               objWallet.modifiedBy = accountId;
                               objWallet.modifiedOn = DateTime.Now;
                               db.SaveChanges();
                           }
                           else
                           {
                               objWallet = new wallet();
                               objWallet.walletBalance = "0";
                               objWallet.accountId = objJob.createdBy;
                               objWallet.createdBy = accountId;
                               objWallet.createdOn = DateTime.Now;
                               objWallet.modifiedBy = accountId;
                               objWallet.modifiedOn = DateTime.Now;
                               objWallet.isDeleted = false;
                               db.wallets.Add(objWallet);
                               db.SaveChanges();
                               walletTransaction objwalletTransaction = new walletTransaction();
                               objwalletTransaction.transactionAmount = Convert.ToString(ConfigurationManager.AppSettings["amountforpost"]);
                               objwalletTransaction.walletId = objWallet.walletId;
                               objwalletTransaction.walletDescription = "Job post successfully approved";
                               objwalletTransaction.createdBy = accountId;
                               objwalletTransaction.createdOn = DateTime.Now;
                               objwalletTransaction.modifiedBy = accountId;
                               objwalletTransaction.modifiedOn = DateTime.Now;
                               objwalletTransaction.transactionStatus = 1;
                               objwalletTransaction.isDeleted = false;
                               db.walletTransactions.Add(objwalletTransaction);
                               objWallet.walletBalance = Convert.ToString(Convert.ToInt64(objWallet.walletBalance) + Convert.ToInt64(ConfigurationManager.AppSettings["amountforpost"]));
                               objWallet.modifiedBy = accountId;
                               objWallet.modifiedOn = DateTime.Now;
                               db.SaveChanges();
                           }
                       }
                   }
                   transaction.Complete();
               }
               catch (Exception ex)
               {
                   transaction.Dispose();
               }
           }
           return objError;
       }
    }
} 
