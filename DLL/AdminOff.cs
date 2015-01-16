using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public class AdminOff
    {
        private offcampus4uEntities db;

        public Error checkAccount(string email, string password)
        {
            Error objError = new Error();
            try
            {
                db = new offcampus4uEntities();
                var userData = (from adm in db.accounts
                                where adm.email.Equals(email) && adm.password.Equals(password) && adm.isAdmin.Equals(true)
                                select adm).SingleOrDefault();
                if (userData != null)
                {
                    db.Dispose();
                    objError.isSuccess = true;
                    objError.message = userData.accountId.ToString();
                    return objError;
                }
                else
                {
                    objError.isSuccess = false;
                    objError.message = "Email or Password is wrong.";
                    return objError;
                }

            }
            catch
            {
                return objError;
            }
        }

        public List<job> getunApproveJobs()
        {
            List<job> listJob = new List<job>();
            db = new offcampus4uEntities();
            listJob = db.jobs.Where(x => x.jobDeteled.Equals(false) && x.jobStatus.Equals(0)).ToList<job>();
            return listJob;
        }

        public Error saveCompany(company objCompany){
            Error objError = new Error();
            objError.isSuccess = false;
            objError.message = "Oops.. somthing went wrong. please try again later.";
            try
            {
                db = new offcampus4uEntities();
                db.companies.Add(objCompany);
                db.SaveChanges();
                objError.isSuccess = true;
                objError.message = "Success";
            }
            catch (Exception ex)
            {
                objError.isSuccess = false;
                objError.message = "Oops.. somthing went wrong. please try again later.";
            }
            return objError;
        }
    }
}
