using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvclibrary.ViewModels;
using DLL;
using System.IO;

namespace mvclibrary.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            Profile objProfile = new Profile();
            ProfileViewModel objProfileViewModel = new ProfileViewModel();
            account objpersonalInformation = objProfile.GetPersonalInformation(Convert.ToInt64(User.Identity.Name));
            UserProfileViewModel objUserProfileViewModel = new UserProfileViewModel();
            Commondetail objEducation = new Commondetail();
          
            if (objpersonalInformation != null)
            {
                objUserProfileViewModel.firstName = (objpersonalInformation.firstName == null) ? "" : objpersonalInformation.firstName;
                objUserProfileViewModel.lastName = objpersonalInformation.lastName == null ? "" : objpersonalInformation.lastName;
                objUserProfileViewModel.emailAddress = objpersonalInformation.email == null ? "" : objpersonalInformation.email;
                objUserProfileViewModel.phoneNumber = objpersonalInformation.phoneNumber == null ? "" : objpersonalInformation.phoneNumber;
                objUserProfileViewModel.basedIn = (objpersonalInformation.basedIn == null) ? "" : objpersonalInformation.basedIn;
                objUserProfileViewModel.classifiedRole = objpersonalInformation.classifiedRole == null ? "" : objpersonalInformation.classifiedRole;
                objUserProfileViewModel.jobseekingStatus = objpersonalInformation.jobseekingStatus == null ? "" : objpersonalInformation.jobseekingStatus;
                objUserProfileViewModel.availability = objpersonalInformation.availability == null ? "" : objpersonalInformation.availability;

                objUserProfileViewModel.highestEducation = objpersonalInformation.highestEducation == null ? "" : objpersonalInformation.highestEducation;
                objUserProfileViewModel.workType = objpersonalInformation.workType == null ? "" : objpersonalInformation.workType;
                objUserProfileViewModel.Preferredlocations = objpersonalInformation.Preferredlocations == null ? "" : objpersonalInformation.Preferredlocations;
                objUserProfileViewModel.ResumeFileName = objpersonalInformation.ResumeFileName == null ? "" : objpersonalInformation.ResumeFileName;
                objUserProfileViewModel.PersonalInformationUpdated = objpersonalInformation.PersonalInformationUpdated;
                objUserProfileViewModel.QualificationSkillsUpdated = objpersonalInformation.QualificationSkillsUpdated;
                objUserProfileViewModel.CurrentStatusUpdated = objpersonalInformation.CurrentStatusUpdated;
                objUserProfileViewModel.ResumeUpdated = objpersonalInformation.ResumeUpdated;
                objProfileViewModel.UserPersonalInformation = objUserProfileViewModel;
            }
            //objProfileViewModel.EducationList = objEducation.getHighestEducation();
            //objProfileViewModel.myeducation = 2;
            
            return View(objProfileViewModel);
        }

        [HttpPost]
        public JsonResult UpdateQualiifcationPreferencesInformation(UserProfileViewModel profile)
        {
            Error objError = new Error();
            if (string.IsNullOrEmpty(profile.highestEducation))
            {
                objError.isSuccess = false;
                objError.message = "Enter your Highest Qualification.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }

            if (string.IsNullOrEmpty(profile.workType))
            {
                objError.isSuccess = false;
                objError.message = "Enter the Work type.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }

            if (string.IsNullOrEmpty(profile.Preferredlocations))
            {
                objError.isSuccess = false;
                objError.message = "Enter the Preserred Locations.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }else
            {
                account account = new account();
                account.accountId = Convert.ToInt64(User.Identity.Name);
                account.highestEducation = profile.highestEducation;
                account.workType = profile.workType;
                account.Preferredlocations = profile.Preferredlocations;
                account.modifiedBy = Convert.ToInt64(User.Identity.Name);
                account.modifiedOn = DateTime.Now;
                Profile objProfile = new Profile();
                objError = objProfile.updateQualiifcationPreferencesInformation(account);
                return Json(objError, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult UpdateCurrentStatusInformation(UserProfileViewModel profile)
        {
            Error objError = new Error();
            if (string.IsNullOrEmpty(profile.basedIn))
            {
                objError.isSuccess = false;
                objError.message = "Enter your current location.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }

            if (string.IsNullOrEmpty(profile.classifiedRole))
            {
                objError.isSuccess = false;
                objError.message = "Enter the role is classified as.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }

            if (string.IsNullOrEmpty(profile.availability))
            {
                objError.isSuccess = false;
                objError.message = "Enter the Availability.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }

            if (string.IsNullOrEmpty(profile.jobseekingStatus))
            {
                objError.isSuccess = false;
                objError.message = "Enter the Jobseeking status.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }
            else
            {
                account account = new account();
                account.accountId = Convert.ToInt64(User.Identity.Name);
                account.basedIn = profile.basedIn;
                account.classifiedRole = profile.classifiedRole;
                account.jobseekingStatus = profile.jobseekingStatus;
                account.availability = profile.availability;
                account.modifiedBy = Convert.ToInt64(User.Identity.Name);
                account.modifiedOn = DateTime.Now;
                Profile objProfile = new Profile();
                objError = objProfile.updateCurrentStatus(account);
                return Json(objError, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult UpdatePersonalInformation(UserProfileViewModel profile)
        {
            Error objError = new Error();
            if (string.IsNullOrEmpty(profile.firstName))
            {
                objError.isSuccess = false;
                objError.message = "Enter the First Name.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }

            if (string.IsNullOrEmpty(profile.lastName))
            {
                objError.isSuccess = false;
                objError.message = "Enter the Last Name.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }

            if (string.IsNullOrEmpty(profile.phoneNumber))
            {
                objError.isSuccess = false;
                objError.message = "Enter the Phone.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }
            else
            {
                account account = new account();
                account.accountId = Convert.ToInt64(User.Identity.Name);
                account.firstName = profile.firstName;
                account.lastName = profile.lastName;
                account.phoneNumber = profile.phoneNumber;
                account.modifiedBy = Convert.ToInt64(User.Identity.Name);
                account.modifiedOn = DateTime.Now;
                Profile objProfile = new Profile();
                objError = objProfile.updatePersonalInformation(account);
                return Json(objError, JsonRequestBehavior.AllowGet);
            }

        }


        public JsonResult UploadResume()
        {
            if (Request.Files.AllKeys.Any())
            {
                // Get the uploaded image from the Files collection
                var httpPostedFile = Request.Files["UploadedImage"];
                // Validate the uploaded image(optional)
                string randonname = Convert.ToString(DateTime.Now.Ticks);
                string[] originalFileName = httpPostedFile.FileName.Split('.');
                if (httpPostedFile != null)
                {
                   

                    // Get the complete file path
                    try
                    {
                        var fileSavePath = Path.Combine(Server.MapPath("~/UserFiles/Resume"), randonname+"."+originalFileName[1]);

                        // Save the uploaded file to "UploadedFiles" folder
                        httpPostedFile.SaveAs(fileSavePath);
                    }
                    catch
                    {
                        Error objError = new Error();
                        objError.isSuccess = false;
                        objError.message = "OOps Something went wrong. please try again later.";
                        return Json(objError, JsonRequestBehavior.AllowGet);
                    }
                    account objAccount = new account();
                    objAccount.accountId = Convert.ToInt64(User.Identity.Name);
                    objAccount.ResumeFileName = randonname + "." + originalFileName[1];
                    objAccount.modifiedBy = Convert.ToInt64(User.Identity.Name);
                    objAccount.modifiedOn = DateTime.Now;
                    Profile objProfile = new Profile();
                    Error objError1 = objProfile.updateResumeFile(objAccount);
                    return Json(objError1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Error objError = new Error();
                    objError.isSuccess = false;
                    objError.message = "Selected Icon size can't be null";
                    return Json(objError, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                Error objError = new Error();
                objError.isSuccess = false;
                objError.message = "Please select Icon for the category";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UpdateUserPassword(string currentpassword, string newpassword, string newpassword1)
        {
            Error objError = new Error();
            if (newpassword.Length < 8 || newpassword1.Length < 8)
            {
                objError.isSuccess = false;
                objError.message = "Password length can't be less than 7 Characters.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(currentpassword) || string.IsNullOrEmpty(newpassword) || string.IsNullOrEmpty(newpassword1))
            {
                objError.isSuccess = false;
                objError.message = "All fields are compulsory.";
            }

            if (newpassword.Equals(newpassword1))
            {
                Accounts objAccount = new Accounts();
                objError = objAccount.changePassword(currentpassword, Convert.ToInt64(User.Identity.Name), newpassword);
            }
            else
            {
                objError.isSuccess = false;
                objError.message = "New Password and Confirm New Password should be match.";
            }
            return Json(objError, JsonRequestBehavior.AllowGet);
        }

    }
}
