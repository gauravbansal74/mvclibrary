using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace DLL
{
   public class Tutorials
    {
       private offcampus4uEntities db;

       public List<tutorialCategory> getCategryList()
       {
           List<tutorialCategory> objTutorialsList = new List<tutorialCategory>();
           db = new offcampus4uEntities();
           objTutorialsList = db.tutorialCategories.Where(x => x.isDeleted == false).ToList<tutorialCategory>();
           return objTutorialsList;
       }

       public List<video> getVideoList(Int64 id)
       {
           db = new offcampus4uEntities();
           List<video> objTutorialsList = db.videos.Where(x => x.categoryId.Equals(id) && x.isDeleted.Equals(false)).ToList<video>();
           return objTutorialsList;
       }


       public List<video> getVideoListNextTen(Int64 id)
       {
           db = new offcampus4uEntities();
           List<video> objTutorialsList = db.videos.Where(x => x.categoryId.Equals(id) && x.videoId > id && x.isDeleted.Equals(false)).OrderBy(x=>x.videoId).Skip(10).Take(10).ToList<video>();
           return objTutorialsList;
       }

       public video getVideo(Int64 id)
       {
           db = new offcampus4uEntities();
           video objTutorialsList = db.videos.Where(x => x.videoId.Equals(id) && x.isDeleted.Equals(false)).FirstOrDefault();
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
