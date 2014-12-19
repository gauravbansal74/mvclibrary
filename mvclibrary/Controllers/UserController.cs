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
            personalInformation objpersonalInformation = objProfile.GetPersonalInformation(Convert.ToInt64(User.Identity.Name));
            UserProfileViewModel objUserProfileViewModel = new UserProfileViewModel();
            if (objUserProfileViewModel != null)
            {
                objUserProfileViewModel.firstName = (objpersonalInformation.firstName == null) ? "" : objpersonalInformation.firstName;
                objUserProfileViewModel.lastName = objpersonalInformation.lastName == null ? "" : objpersonalInformation.lastName;
                objUserProfileViewModel.emailAddress = objpersonalInformation.emailAddress == null ? "" : objpersonalInformation.emailAddress;
                objUserProfileViewModel.phoneNumber = objpersonalInformation.phoneNumber == null ? "" : objpersonalInformation.phoneNumber;
                objProfileViewModel.UserPersonalInformation = objUserProfileViewModel;
            }
            
            
            return View(objProfileViewModel);
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
                personalInformation personalInformation = new personalInformation();
                personalInformation.accountId = Convert.ToInt64(User.Identity.Name);
                personalInformation.firstName = profile.firstName;
                personalInformation.lastName = profile.lastName;
                personalInformation.phoneNumber = profile.phoneNumber;
                personalInformation.createdBy= Convert.ToInt64(User.Identity.Name);
                personalInformation.createdOn = DateTime.Now;
                personalInformation.modifiedBy = Convert.ToInt64(User.Identity.Name);
                personalInformation.modifiedOn = DateTime.Now;
                Profile objProfile = new Profile();
                objError = objProfile.updatePersonalInformation(personalInformation);
                return Json(objError, JsonRequestBehavior.AllowGet);
            }


             
             objError.isSuccess = false;
             objError.message = "Enter the email address.";
             return Json(objError, JsonRequestBehavior.AllowGet);
        }

    }
}
