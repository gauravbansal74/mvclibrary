using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
   public class Tutorials
    {
       private offcampus4uEntities db;

       public List<tutorialCategory> getCategryList()
       {
           db = new offcampus4uEntities();
           List<tutorialCategory> objTutorialsList = db.tutorialCategories.Where(x => x.isDeleted == false).ToList<tutorialCategory>();
           return objTutorialsList;
       }

       public Error saveCategory(tutorialCategory category)
       {
           Error objError = new Error();
           try
           {
               db = new offcampus4uEntities();
               db.tutorialCategories.Add(category);
               db.SaveChanges();
               objError.isSuccess = true;
               objError.message = "Category Successfully Added.";
           }
           catch
           {
               objError.isSuccess = false;
               objError.message = "Oops.. Something went wrong. please try again after sometime.";
           }
           return objError;
       }


       public Error saveVideo(video video)
       {
           Error objError = new Error();
           try
           {
               db = new offcampus4uEntities();
               db.videos.Add(video);
               db.SaveChanges();
               objError.isSuccess = true;
               objError.message = "Video Successfully Added.";
           }
           catch
           {
               objError.isSuccess = false;
               objError.message = "Oops.. Something went wrong. please try again after sometime.";
           }
           return objError;
       }
    }
}
