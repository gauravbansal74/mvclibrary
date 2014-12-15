using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DLL
{
    public class Accounts
    {
        private offcampus4uEntities db;

        public bool registerAccount(account account)
        {
            db.accounts.Add(account);
            db.SaveChanges();
            return false;
        }
    }
}
