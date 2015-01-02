using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DLL
{
    public class Jobs
    {
        private offcampus4uEntities db;

        public List<job> getJobs(int skipRecords, int limit)
        {

            List<job> listJob = new List<job>();
            db = new offcampus4uEntities();
            db.Configuration.ProxyCreationEnabled = false;
            listJob = db.jobs.Where(x => x.jobDeteled.Equals(false) && x.jobStatus.Equals(1)).OrderByDescending(x => x.jobId).Skip(skipRecords).Take(limit).ToList<job>();
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

       

        public List<job> searchJob(string skillsdesignationcompany, string location, string experience, string salary, int skiprecords, int limit)
        {
            List<job> listJob = new List<job>();
            offcampus4uEntities db = new DLL.offcampus4uEntities();
            db = new offcampus4uEntities();
            db.Configuration.ProxyCreationEnabled = false;
            var queryableJob = db.jobs.Where(x => x.jobStatus.Equals(1));
            if (!string.IsNullOrWhiteSpace(skillsdesignationcompany))
            {
                queryableJob = queryableJob.Where(x => x.jobCompnayName.Contains(skillsdesignationcompany) || x.jobDescription.Contains(skillsdesignationcompany) || x.jobTitle.Contains(skillsdesignationcompany));
            }

            if (!string.IsNullOrEmpty(location))
            {
                queryableJob = queryableJob.Where(x => x.jobLocation.Contains(location));
            }

            if (!string.IsNullOrEmpty(experience))
            {
                Int64 exp = Convert.ToInt64(experience);
                queryableJob = queryableJob.Where(x => x.jobMaxExp <= exp || x.jobMaxExp >= exp);
            }

            if (!string.IsNullOrEmpty(salary))
            {
                Int64 sal = Convert.ToInt64(salary);
                queryableJob = queryableJob.Where(x => x.jobMaxSalary <= sal || x.jobMaxSalary >= sal);
            }

            listJob = queryableJob.OrderByDescending(x => x.jobId).Skip(skiprecords).Take(limit).ToList<job>();
            return listJob;
        }

       

        public List<job> searchJobCustom(string skillsdesignationcompany, string location, string experience, string salary)
        {
            List<job> listJob = new List<job>();
            db = new offcampus4uEntities();
            if (!string.IsNullOrWhiteSpace(skillsdesignationcompany) && !string.IsNullOrWhiteSpace(skillsdesignationcompany) && !string.IsNullOrEmpty(location) && !string.IsNullOrEmpty(experience))
            {
                Int64 myexperience = Convert.ToInt64(experience);
                Int64 package = Convert.ToInt64(salary);
                listJob = db.jobs.Where(x => x.jobCompnayName.Contains(skillsdesignationcompany) || x.jobDescription.Contains(skillsdesignationcompany) || x.jobTitle.Contains(skillsdesignationcompany) || x.jobLocation.Contains(location) || (x.jobMaxExp <= myexperience && x.jobMinSalary >= package)).OrderByDescending(x => x.jobId).ToList<job>();
            }

            if (string.IsNullOrEmpty(skillsdesignationcompany) && !string.IsNullOrEmpty(location) && string.IsNullOrEmpty(experience))
            {
                listJob = db.jobs.Where(x => x.jobLocation.Contains(location)).OrderByDescending(x => x.jobId).ToList<job>();
            }

            if (string.IsNullOrEmpty(location) && !string.IsNullOrEmpty(skillsdesignationcompany) && string.IsNullOrEmpty(experience))
            {
                listJob = db.jobs.Where(x => x.jobCompnayName.Contains(skillsdesignationcompany) || x.jobDescription.Contains(skillsdesignationcompany) || x.jobTitle.Contains(skillsdesignationcompany) || x.jobLocation.Contains(location)).OrderByDescending(x => x.jobId).ToList<job>();
            }
            if (string.IsNullOrEmpty(location) && string.IsNullOrEmpty(salary) && string.IsNullOrEmpty(skillsdesignationcompany) && !string.IsNullOrEmpty(experience))
            {
                Int32 myexperience = Convert.ToInt32(experience);
                switch (myexperience)
                {
                    case 0:
                        listJob = db.jobs.Where(x => x.jobMaxExp <= myexperience).OrderByDescending(x => x.jobId).ToList<job>();
                        break;
                    case 2:
                        listJob = db.jobs.Where(x => x.jobMaxExp >= myexperience && x.jobMaxExp < 4).OrderByDescending(x => x.jobId).ToList<job>();
                        break;
                    case 4:
                        listJob = db.jobs.Where(x => x.jobMaxExp >= myexperience && x.jobMaxExp < 6).OrderByDescending(x => x.jobId).ToList<job>();
                        break;
                    case 6:
                        listJob = db.jobs.Where(x => x.jobMaxExp >= myexperience && x.jobMaxExp < 8).OrderByDescending(x => x.jobId).ToList<job>();
                        break;
                    case 8:
                        listJob = db.jobs.Where(x => x.jobMaxExp >= myexperience && x.jobMaxExp < 10).OrderByDescending(x => x.jobId).ToList<job>();
                        break;
                    case 10:
                        listJob = db.jobs.Where(x => x.jobMaxExp >= myexperience).OrderByDescending(x => x.jobId).ToList<job>();
                        break;
                }

            }

            return listJob;
        }

        public Error saveJob(job job)
        {
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
            catch (Exception ex)
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
                account objAccount = db.accounts.Where(x => x.accountId.Equals(userid)).FirstOrDefault();
                if (objAccount != null)
                {
                    if (string.IsNullOrEmpty(objAccount.ResumeFileName))
                    {
                        objError.isSuccess = false;
                        objError.message = "Update your profile to apply for this job.";
                    }
                    else
                    {
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
                }
                else
                {
                    objError.isSuccess = false;
                    objError.message = "Oops. somthing went wrong. please try again later.";
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

            using (var transaction = new TransactionScope())
            {
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
