using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DLL
{
    public class Accounts
    {
        private offcampus4uEntities db;

        public account getAccount(Int64 userId)
        {
            db = new offcampus4uEntities();
            account objAccount = db.accounts.Where(x => x.accountId == userId).FirstOrDefault();
            return objAccount;
        }

        public Error registerAccount(account account)
        {
            Error objError = new Error();
            try
            {
                db = new offcampus4uEntities();
                db.accounts.Add(account);
                db.SaveChanges();
                db.Dispose();
                objError.isSuccess = true;
                objError.message = "We have created your account. please verify your email address.";
                return objError;
            }
            catch
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
                var userData = (from user in db.accounts
                             where user.email.Equals(email)
                             select user).SingleOrDefault();
                db.Dispose();
                objError.isSuccess = true;
                objError.message = userData.accountId.ToString();
                return objError;
            }
            catch
            {
                return objError;
            }
        }
    }
}