using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DLL;
using System.Threading.Tasks;
using System.Net.Mail;

namespace admin.Controllers
{
    public class EmailNotifier
    {
        public Error sendEmail(string email, string body, string subject){
            Error objError = new Error();
            Task.Factory.StartNew(() =>
            {
                try
                {

                    MailMessage mail = new MailMessage();
                    mail.To.Add(new MailAddress(email));
                    mail.From = new MailAddress(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["adminemail"]));
                    mail.Subject = subject;
                    string Body = body;
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
                    objError.isSuccess = true;
                    objError.message = "We have sent you password on your registred email address.";
                }
                catch
                {
                    objError.isSuccess = false;
                    objError.message = "OOps.. somthing went wrong. please try again after sometime.";
                }
            });
            objError.isSuccess = true;
            objError.message = "We have sent you password on your registred email address.";
            return objError;
        }

        public Error getUserEmail(Int64 userId)
        {
           
            Error objError = new Error();
            objError.isSuccess = false;
            try
            {
                objError.message = "Oops something went wrong please try again later";
                Accounts objAccounts = new Accounts();
                account objAccount = objAccounts.getAccount(userId);
                if (objAccount != null)
                {
                    objError.isSuccess = true;
                    objError.message = objAccount.email;
                }
            }catch{
            }
            return objError;
        }
    }
}