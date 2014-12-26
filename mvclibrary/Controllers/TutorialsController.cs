using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLL;
using System.IO;
using mvclibrary.ViewModels;

namespace mvclibrary.Controllers
{
    public class TutorialsController : Controller
    {
        //
        // GET: /Course/
      [AllowAnonymous]
        public ActionResult Index()
        {
            Tutorials objTutorials = new Tutorials();
            List<tutorialCategory> objCatList = objTutorials.getCategryList();
            return View(objCatList);
        }

        public ActionResult seeTutorial(Int64 id, int catId)
        {
            VideoViewModel objVideoViewModel = new VideoViewModel();
            Tutorials objTutorials = new Tutorials();
            objVideoViewModel.currentVideo = objTutorials.getVideo(id);
            objVideoViewModel.videoList = objTutorials.getVideoListNextTen(catId);
            return View(objVideoViewModel);
        }
       
        [AllowAnonymous]
        public ActionResult Tutorial(Int64 id)
        {
            TutorialViewModel objTutorialViewModel = new TutorialViewModel();
            Tutorials objTutorials = new Tutorials();
            objTutorialViewModel.videoList = objTutorials.getVideoList(id);
            objTutorialViewModel.tutorialCategoryList = objTutorials.getCategryList();
            if (objTutorialViewModel.videoList.Count > 0)
            {
                ViewBag.PageTitle = objTutorialViewModel.videoList[0].tutorialCategory.tutorialCategoryName;
            }
            else
            {
                ViewBag.PageTitle = "Tutorials";
            }
            return View(objTutorialViewModel);
        }

        public ActionResult Category()
        {
            Tutorials objTutorials = new Tutorials();
            List<tutorialCategory> objCatList = objTutorials.getCategryList();
            return View(objCatList);
        }

        public JsonResult saveCategory(string tutorialCategoryName)
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
                        var fileSavePath = Path.Combine(Server.MapPath("~/AppImages/TutorialCategory"), httpPostedFile.FileName);

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
                    tutorialCategory objTutorialCategory = new tutorialCategory();
                    objTutorialCategory.tutorialCategoryName = tutorialCategoryName;
                    objTutorialCategory.tutorialCategoryFileName = httpPostedFile.FileName;
                    objTutorialCategory.createdBy = Convert.ToInt64(User.Identity.Name);
                    objTutorialCategory.createdOn = DateTime.Now;
                    objTutorialCategory.modifiedBy = Convert.ToInt64(User.Identity.Name);
                    objTutorialCategory.modifiedOn = DateTime.Now;
                    objTutorialCategory.isDeleted = false;
                    objTutorialCategory.tutorialCategoryStatus = 1;
                    Tutorials objTutorials = new Tutorials();
                    Error objError1 = objTutorials.saveCategory(objTutorialCategory);
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

        public JsonResult saveVideo(Int64 categoryId, string videoTitle, string videoYoutubeId)
        {
            video objVideo = new video();
            objVideo.videoTitle = videoTitle;
            objVideo.videoYoutubeId = videoYoutubeId;
            objVideo.categoryId = categoryId;
            objVideo.createdBy = Convert.ToInt64(User.Identity.Name);
            objVideo.createdOn = DateTime.Now;
            objVideo.modifiedBy = Convert.ToInt64(User.Identity.Name);
            objVideo.modifiedOn = DateTime.Now;
            objVideo.isDeleted = false;
            Tutorials objTutorials = new Tutorials();
            Error objError = objTutorials.saveVideo(objVideo);
            return Json(objError, JsonRequestBehavior.AllowGet);
        }



    }
}
