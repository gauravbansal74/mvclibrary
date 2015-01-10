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
                var userData = (from emp in db.admins
                                where emp.adminEmail.Equals(email) && emp.adminPassword.Equals(password)
                                select emp).SingleOrDefault();
                if (userData != null)
                {
                    db.Dispose();
                    objError.isSuccess = true;
                    objError.message = userData.adminId.ToString();
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
