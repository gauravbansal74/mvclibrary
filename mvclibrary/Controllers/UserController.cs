using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvclibrary.ViewModels;
using DLL;
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
            if (objUserProfileViewModel != null)
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
                
                objProfileViewModel.UserPersonalInformation = objUserProfileViewModel;
            }
            
            
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

    }
}
