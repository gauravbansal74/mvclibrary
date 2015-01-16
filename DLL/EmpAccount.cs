using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public class EmpAccount
    {
        private offcampus4uEntities db;

        public account getEmployerDetail(Int64 userid)
        {
            account objAccount = new account();
            try
            {
                db = new offcampus4uEntities();
                objAccount = db.accounts.Where(x => x.accountId.Equals(userid) && x.isEmployer.Equals(true)).FirstOrDefault();
                return objAccount;
            }
            catch (Exception ex)
            {
            }
            return objAccount;
        }

        public company getEmployerCompany(Int64 userid)
        {
            db = new offcampus4uEntities();
            company objCompany = (from com in db.companies
                                  join acc in db.accounts on com.companyId equals acc.companyId
                                  where acc.accountId.Equals(userid)
                                  select com).FirstOrDefault();
            return objCompany;
        }
    }
}


//var entryPoint = (from ep in dbContext.tbl_EntryPoint
//                 join e in dbContext.tbl_Entry on ep.EID equals e.EID
//                 join t in dbContext.tbl_Title on e.TID equals t.TID
//                 where e.OwnerID == user.UID
//                 select new {
//                     UID = e.OwnerID,
//                     TID = e.TID,
//                     Title = t.Title,
//                     EID = e.EID
//                 }).Take(10);