using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public class Profile
    {
        private offcampus4uEntities db;

        public personalInformation GetPersonalInformation(Int64 accountId)
        {
            db = new offcampus4uEntities();
            personalInformation objPersonalInformation = db.personalInformations.Where(x => x.accountId == accountId).FirstOrDefault();
            return objPersonalInformation;
        }

        public Error updatePersonalInformation(personalInformation personal)
        {
            Error objError = new Error();
            try
            {
                db = new offcampus4uEntities();
                personalInformation objPersonalInformation = db.personalInformations.Where(x=>x.accountId == personal.accountId).FirstOrDefault();
                if(objPersonalInformation != null){
                    objPersonalInformation.emailAddress = objPersonalInformation.account.email;
                    objPersonalInformation.firstName = personal.firstName;
                    objPersonalInformation.lastName = personal.lastName;
                    objPersonalInformation.phoneNumber = personal.phoneNumber;
                    objPersonalInformation.modifiedBy = personal.modifiedBy;
                    objPersonalInformation.modifiedOn = personal.modifiedOn;
                    db.SaveChanges();
                    objError.isSuccess = true;
                    objError.message = "Personal Information successfully updated";
                    return objError;
                }else{
                db.personalInformations.Add(personal);
                db.SaveChanges();
                objError.isSuccess = true;
                objError.message = "Personal Information successfully updated";
                return objError;
                }
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
