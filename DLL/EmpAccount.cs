using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public class EmpAccount
    {
        private offcampus4uEntities db;

        public Error registerAccount(employer employer)
        {
            Error objError = new Error();
            try
            {
                db = new offcampus4uEntities();
                employer objaccount = db.employers.Where(x => x.employerEmail.Equals(employer.employerEmail)).FirstOrDefault();
                if (objaccount == null)
                {
                    employer.employerKey = Convert.ToString(DateTime.Now.Ticks);
                    db.employers.Add(employer);
                    db.SaveChanges();
                    db.Dispose();
                    objError.isSuccess = true;
                    objError.message = employer.employerKey;
                    return objError;
                }
                else
                {
                    objError.isSuccess = false;
                    objError.message = "Employer is already registred with this email address.";
                    return objError;
                }
            }
            catch(Exception e)
            {
                objError.isSuccess = false;
                objError.message = "Oops, something went wrong. please try again after sometime.";
                return objError;
            }
        }

        public Error checkAccount(string email, string password)
        {
            Error objError = new Error();
            try
            {
                db = new offcampus4uEntities();
                var userData = (from emp in db.employers
                                where emp.employerEmail.Equals(email) && emp.employerPassword.Equals(password)
                                select emp).SingleOrDefault();
                if (userData != null)
                {
                    db.Dispose();
                    objError.isSuccess = true;
                    objError.message = userData.employerId.ToString();
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
    }
}
