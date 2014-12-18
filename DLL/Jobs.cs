
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
