using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLL;

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

        public ActionResult seeTutorial(Int64 id)
        {
            Tutorials objTutorials = new Tutorials();
            video objCatList = objTutorials.getVideo(id);
            return View(objCatList);
        }
       
        [AllowAnonymous]
        public ActionResult Tutorial(Int64 id)
        {
            Tutorials objTutorials = new Tutorials();
            List<video> objCatList = objTutorials.getVideoList(id);
            return View(objCatList);
        }

        public ActionResult Category()
        {
            Tutorials objTutorials = new Tutorials();
            List<tutorialCategory> objCatList = objTutorials.getCategryList();
            return View(objCatList);
        }

        public JsonResult saveCategory(string tutorialCategoryName)
        {
            tutorialCategory objTutorialCategory = new tutorialCategory();
            objTutorialCategory.tutorialCategoryName = tutorialCategoryName;
            objTutorialCategory.createdBy = Convert.ToInt64(User.Identity.Name);
            objTutorialCategory.createdOn = DateTime.Now;
            objTutorialCategory.modifiedBy = Convert.ToInt64(User.Identity.Name);
            objTutorialCategory.modifiedOn = DateTime.Now;
            objTutorialCategory.isDeleted = false;
            objTutorialCategory.tutorialCategoryStatus = 1;
            Tutorials objTutorials = new Tutorials();
            Error objError  = objTutorials.saveCategory(objTutorialCategory);
            return Json(objError, JsonRequestBehavior.AllowGet);
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
