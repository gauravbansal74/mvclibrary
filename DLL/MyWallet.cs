using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public class MyWallet
    {
        private offcampus4uEntities db;

        public wallet getmyWallet(Int64 id){
            db = new offcampus4uEntities();
            wallet objWallet = db.wallets.Where(x => x.accountId.Equals(id)).FirstOrDefault();
            return objWallet;
        }

        public List<walletTransaction> getWalletTransactions(Int64 id)
        {
            db = new offcampus4uEntities();
            List<walletTransaction> objWallet = db.walletTransactions.Where(x => x.walletId.Equals(id)).ToList<walletTransaction>();
            return objWallet;
        }
    }
}
