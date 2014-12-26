using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public class Commondetail
    {
        private offcampus4uEntities db; 

        public List<highestEducation> getHighestEducation(){
            db = new offcampus4uEntities();
            List<highestEducation> listHighestEducation = db.highestEducations.Where(x => x.isDeleted.Equals(false)).ToList<highestEducation>();
            return listHighestEducation;
        }
    }
}
