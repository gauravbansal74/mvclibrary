
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
   public class Jobs
    {
       private offcampus4uEntities db;

       public List<job> getJobs()
       {
           List<job> listJob = new List<job>();
           db = new offcampus4uEntities();
           listJob = db.jobs.Where(x => x.jobDeteled.Equals(false)).ToList<job>();
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
               listJob = db.jobs.Where(x => x.jobCompnayName.Contains(skillsdesignationcompany) || x.jobDescription.Contains(skillsdesignationcompany) || x.jobTitle.Contains(skillsdesignationcompany) || x.jobLocation.Contains(location) || x.jobMinExp <= experience || x.jobMaxExp >= experience || x.jobMinSalary <= salary || x.jobMaxSalary >= salary).ToList<job>();
           }

           if (string.IsNullOrEmpty(skillsdesignationcompany) && !string.IsNullOrEmpty(location))
           {
               listJob = db.jobs.Where(x => x.jobLocation.Contains(location) || x.jobMinExp <= experience || x.jobMaxExp >= experience || x.jobMinSalary <= salary || x.jobMaxSalary >= salary).ToList<job>();
           }

           if (string.IsNullOrEmpty(location) && !string.IsNullOrEmpty(skillsdesignationcompany))
           {
               listJob = db.jobs.Where(x => x.jobCompnayName.Contains(skillsdesignationcompany) || x.jobDescription.Contains(skillsdesignationcompany) || x.jobTitle.Contains(skillsdesignationcompany) || x.jobLocation.Contains(location) || x.jobMinExp <= experience || x.jobMaxExp >= experience || x.jobMinSalary <= salary || x.jobMaxSalary >= salary).ToList<job>();
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
               listJob = db.jobs.Where(x => x.jobCompnayName.Contains(skillsdesignationcompany) || x.jobDescription.Contains(skillsdesignationcompany) || x.jobTitle.Contains(skillsdesignationcompany) || x.jobLocation.Contains(location)).ToList<job>();
           }

           if (string.IsNullOrEmpty(skillsdesignationcompany) && !string.IsNullOrEmpty(location))
           {
               listJob = db.jobs.Where(x => x.jobLocation.Contains(location)).ToList<job>();
           }

           if (string.IsNullOrEmpty(location) && !string.IsNullOrEmpty(skillsdesignationcompany))
           {
               listJob = db.jobs.Where(x => x.jobCompnayName.Contains(skillsdesignationcompany) || x.jobDescription.Contains(skillsdesignationcompany) || x.jobTitle.Contains(skillsdesignationcompany) || x.jobLocation.Contains(location)).ToList<job>();
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
           catch
           {
               objError.isSuccess = false;
               objError.message = "Oops.. Somthing went wrong. please try again after sometime.";
               return objError;
           }
       }
    }
}
