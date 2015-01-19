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

        public string getPasswordUsingEmail(string email)
        {
            db = new offcampus4uEntities();
            account objAccount = db.accounts.Where(x => x.email == email).FirstOrDefault();
            return objAccount.password;
        }

        public Error registerAccount(account account)
        {
            Error objError = new Error();
            try
            {
                db = new offcampus4uEntities();
                account objaccount = db.accounts.Where(x => x.email == account.email).FirstOrDefault();
                if (objaccount == null)
                {
                    account.accountKey = Convert.ToString(DateTime.Now.Ticks);
                    db.accounts.Add(account);
                    db.SaveChanges();
                    db.Dispose();
                    objError.isSuccess = true;
                    objError.message = account.accountKey;
                    return objError;
                }
                else
                {
                    objError.isSuccess = false;
                    objError.message = "Account is already registred with this email address.";
                    return objError;
                }
            }
            catch
            {
                objError.isSuccess = false;
                objError.message = "Oops, something went wrong. please try again after sometime.";
                return objError;
            }
        }

        public Error checkAccountWithGmail(string email, string first_name, string last_name, string id)
        {
            Error objError = new Error();
            try
            {
                db = new offcampus4uEntities();
                account userData = (from user in db.accounts
                                where user.email.Equals(email)
                                select user).SingleOrDefault();
                if (userData != null)
                {
                    db.Dispose();
                    objError.isSuccess = true;
                    objError.message = userData.accountId.ToString();
                    return objError;
                }
                else
                {
                    account objaccount = new account();
                    objaccount.email = email;
                    objaccount.firstName = first_name;
                    objaccount.lastName = last_name;
                    objaccount.accountStatus = 1;
                    objaccount.createdOn = DateTime.Now;
                    objaccount.email = email;
                    objaccount.password = id;
                    objaccount.isDeleted = false;
                    objaccount.modifiedBy = 1;
                    objaccount.modifiedOn = DateTime.Now;
                    objaccount.accountKey = Convert.ToString(DateTime.Now.Ticks);
                    objaccount = db.accounts.Add(objaccount);
                    db.SaveChanges();
                    db.Dispose();
                    objError.isSuccess = true;
                    objError.message = objaccount.accountId.ToString();
                    return objError;
                }

            }
            catch
            {
                objError.isSuccess = false;
                objError.message = "OOps.. Something went wrong. please try again later.";
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
                             where user.email.Equals(email) && user.password.Equals(password)
                             select user).SingleOrDefault();
                if (userData != null)
                {
                    db.Dispose();
                    objError.isSuccess = true;
                    objError.message = userData.accountId.ToString();
                    return objError;
                }
                else {
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

        public Error verifyEmail(string key)
        {
            Error objError = new Error();
            try
            {
                db = new offcampus4uEntities();
                account userData = (from user in db.accounts
                                where user.accountKey.Equals(key)
                                select user).SingleOrDefault();
                if (userData != null)
                {
                    userData.accountStatus = 1;
                    db.SaveChanges();
                    db.Dispose();
                    objError.isSuccess = true;
                    objError.message = "Your email address successfully verified.";
                    return objError;
                }
                else
                {
                    objError.isSuccess = false;
                    objError.message = "Oops.. something went wrong. please contact to our support team.";
                    return objError;
                }

            }
            catch
            {
                objError.isSuccess = false;
                objError.message = "Oops.. something went wrong. please contact to our support team.";
                return objError;
            }
        }

        public Error changePassword(string currentpassword, long userid, string newpassword)
        {
            Error objError = new Error();
            try
            {
                db = new offcampus4uEntities();
                account userData = (from user in db.accounts
                                where user.accountId.Equals(userid) && user.password.Equals(currentpassword)
                                select user).SingleOrDefault();
                if (userData != null)
                {
                    userData.password = newpassword;
                    db.SaveChanges();
                    db.Dispose();
                    objError.isSuccess = true;
                    objError.message = "Password successfully updated.";
                }
                else
                {
                    objError.isSuccess = false;
                    objError.message = "Current Password is invaild.";
                }

            }
            catch
            {
                objError.isSuccess = true;
                objError.message = "OOps. something went wrong. please try again after sometime.";
            }
            return objError;
        }

        public Error checkAccountForEmployer(string email, string password)
        {
            Error objError = new Error();
            try
            {
                db = new offcampus4uEntities();
                var userData = (from user in db.accounts
                                where user.email.Equals(email) && user.password.Equals(password) && user.accountStatus.Equals(1) && user.isEmployerVerified.Equals(true)
                                select user).SingleOrDefault();
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
            catch(Exception ex)
            {
                objError.isSuccess = false;
                objError.message = ex.Message;
                return objError;
            }
        }
    }
}