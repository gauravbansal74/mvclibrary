using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLL;
using System.IO;

namespace admin.Controllers
{
    public class CompanyController : Controller
    {
        //
        // GET: /Company/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult saveCompany()
        {
            return View();
        }

        public JsonResult saveCompanyData(string companyName, string aboutCompany, string companyWebsite)
        {
            Error objError = new Error();
            if (string.IsNullOrEmpty(companyName) || string.IsNullOrEmpty(aboutCompany) || string.IsNullOrEmpty(companyWebsite))
            {
                objError.isSuccess = false;
                objError.message = "All Fields are mendatory.";
            }
            else
            {
                if (Request.Files.AllKeys.Any())
                {
                    // Get the uploaded image from the Files collection
                    var httpPostedFile = Request.Files["UploadedImage"];

                    if (httpPostedFile != null)
                    {
                        // Validate the uploaded image(optional)

                        // Get the complete file path
                        try
                        {
                            var fileSavePath = Path.Combine(Server.MapPath("~/AppImages/CompanyImage"), httpPostedFile.FileName);

                            // Save the uploaded file to "UploadedFiles" folder
                            httpPostedFile.SaveAs(fileSavePath);
                        }
                        catch
                        {
                            objError.isSuccess = false;
                            objError.message = "OOps Something went wrong. please try again later.";
                            return Json(objError, JsonRequestBehavior.AllowGet);
                        }
                        company objCompany = new company();
                        objCompany.companyName = companyName;
                        objCompany.companyAbout = aboutCompany;
                        objCompany.companyWebsite = companyWebsite;
                        objCompany.companyLogo = httpPostedFile.FileName;
                        objCompany.createdBy = Convert.ToInt64(User.Identity.Name);
                        objCompany.createdOn = DateTime.Now;
                        objCompany.modifiedBy = Convert.ToInt64(User.Identity.Name);
                        objCompany.modifiedOn = DateTime.Now;
                        AdminOff objAdminOff = new AdminOff();
                        objError =objAdminOff.saveCompany(objCompany);
                        return Json(objError, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                  
                        objError.isSuccess = false;
                        objError.message = "Selected Icon size can't be null";
                        return Json(objError, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                  
                    objError.isSuccess = false;
                    objError.message = "Please select Icon for the Company";
                    return Json(objError, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(objError, JsonRequestBehavior.AllowGet);
        }

    }
}
