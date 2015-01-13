using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public class JobRating
    {
        private offcampus4uEntities db;

        public Error saveRating(Int64 jobId, Int64 accountId, int ratingValue)
        {
            Error objError = new Error();
            objError.isSuccess = false;
            objError.message = "Oops Something went wrong. please try again later.";
            db = new offcampus4uEntities();
            try
            {
                Rating objRating = db.Ratings.Where(x => x.JobId.Equals(jobId) && x.createdBy.Equals(accountId)).FirstOrDefault();
                if (objRating != null)
                {
                    objRating.RatingValue = ratingValue;
                    objRating.modifiedBy = accountId;
                    objRating.modifiedOn = DateTime.Now;
                    db.SaveChanges();
                    objError.isSuccess = true;
                    objError.message = "Rating Successfully Saved";
                }
                else
                {
                    Rating objRate = new Rating();
                    objRate.JobId = jobId;
                    objRate.RatingValue = ratingValue;
                    objRate.createdBy = accountId;
                    objRate.createdOn = DateTime.Now;
                    objRate.modifiedBy = accountId;
                    objRate.modifiedOn = DateTime.Now;
                    db.Ratings.Add(objRate);
                    db.SaveChanges();
                    objError.isSuccess = true;
                    objError.message = "Rating Successfully Saved";
                }
            }
            catch (Exception ex)
            {
                objError.isSuccess = false;
                objError.message = "Oops Something went wrong. please try again later.";
            }
            return objError;
        }
        
    }
}
