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

        public account GetPersonalInformation(Int64 accountId)
        {
            db = new offcampus4uEntities();
            account objPersonalInformation = db.accounts.Where(x => x.accountId == accountId).FirstOrDefault();
            return objPersonalInformation;
        }

        public Error updatePersonalInformation(account account)
        {
            Error objError = new Error();
            try
            {
                db = new offcampus4uEntities();
                account objPersonalInformation = db.accounts.Where(x=>x.accountId == account.accountId).FirstOrDefault();
                objPersonalInformation.email = objPersonalInformation.email;
                objPersonalInformation.firstName = account.firstName;
                objPersonalInformation.lastName = account.lastName;
                objPersonalInformation.phoneNumber = account.phoneNumber;
                objPersonalInformation.PersonalInformationUpdated = true;
                objPersonalInformation.modifiedBy = account.modifiedBy;
                objPersonalInformation.modifiedOn = account.modifiedOn;
                db.SaveChanges();
                objError.isSuccess = true;
                objError.message = "Personal Information successfully updated";
                return objError;
            }
            catch
            {
                objError.isSuccess = false;
                objError.message = "Oops.. Somthing went wrong. please try again after sometime.";
                return objError;
            }
        }

        public Error updateCurrentStatus(account account)
        {
            Error objError = new Error();
            try
            {
                db = new offcampus4uEntities();
                account objPersonalInformation = db.accounts.Where(x => x.accountId == account.accountId).FirstOrDefault();
                objPersonalInformation.basedIn = account.basedIn;
                objPersonalInformation.classifiedRole = account.classifiedRole;
                objPersonalInformation.jobseekingStatus = account.jobseekingStatus;
                objPersonalInformation.availability = account.availability;
                objPersonalInformation.CurrentStatusUpdated = true;
                objPersonalInformation.modifiedBy = account.modifiedBy;
                objPersonalInformation.modifiedOn = account.modifiedOn;
                db.SaveChanges();
                objError.isSuccess = true;
                objError.message = "Current Status Information successfully updated";
                return objError;
            }
            catch
            {
                objError.isSuccess = false;
                objError.message = "Oops.. Somthing went wrong. please try again after sometime.";
                return objError;
            }
        }

        public Error updateQualiifcationPreferencesInformation(account account)
        {
            Error objError = new Error();
            try
            {
                db = new offcampus4uEntities();
                account objPersonalInformation = db.accounts.Where(x => x.accountId == account.accountId).FirstOrDefault();
                objPersonalInformation.highestEducation = account.highestEducation;
                objPersonalInformation.workType = account.workType;
                objPersonalInformation.Preferredlocations = account.Preferredlocations;
                objPersonalInformation.QualificationSkillsUpdated = true;
                objPersonalInformation.modifiedBy = account.modifiedBy;
                objPersonalInformation.modifiedOn = account.modifiedOn;
                db.SaveChanges();
                objError.isSuccess = true;
                objError.message = "Qualification & Role preferences Information successfully updated";
                return objError;
            }
            catch
            {
                objError.isSuccess = false;
                objError.message = "Oops.. Somthing went wrong. please try again after sometime.";
                return objError;
            }
        }


        public Error updateResumeFile(account account)
        {
            Error objError = new Error();
            try
            {
                db = new offcampus4uEntities();
                account objPersonalInformation = db.accounts.Where(x => x.accountId == account.accountId).FirstOrDefault();
                objPersonalInformation.ResumeFileName = account.ResumeFileName;
                objPersonalInformation.ResumeUpdated = true;
                objPersonalInformation.modifiedBy = account.modifiedBy;
                objPersonalInformation.modifiedOn = account.modifiedOn;
                db.SaveChanges();
                objError.isSuccess = true;
                objError.message = "Resume successfully uploaded and updated to your profile.";
                return objError;
            }
            catch
            {
                objError.isSuccess = false;
                objError.message = "Oops.. Somthing went wrong. please try again after sometime.";
                return objError;
            }
        }

        public BankDetail getBankDetail(Int64 id)
        {
            db = new offcampus4uEntities();
            BankDetail objPersonalInformation = db.BankDetails.Where(x => x.accountId.Equals(id)).FirstOrDefault();
            return objPersonalInformation;
        }

        public Error updateBankDetail(BankDetail bank)
        {
            Error objError = new Error();
            try
            {
                db = new offcampus4uEntities();
                BankDetail objBankDetail = db.BankDetails.Where(x => x.accountId == bank.accountId).FirstOrDefault();
                if (objBankDetail != null)
                {
                    objBankDetail.BankAccountName = bank.BankAccountName;
                    objBankDetail.BankAccountNumber = bank.BankAccountNumber;
                    objBankDetail.BankIFSCCode = bank.BankIFSCCode;
                    objBankDetail.modifiedBy = bank.modifiedBy;
                    objBankDetail.modifiedOn = bank.modifiedOn;
                    db.SaveChanges();
                    objError.isSuccess = true;
                    objError.message = "Bank account Details Succesfully saved.";
                    return objError;
                }
                else
                {
                    db.BankDetails.Add(bank);
                    db.SaveChanges();
                    objError.isSuccess = true;
                    objError.message = "Bank account Details Succesfully saved.";
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
