using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DLL;

namespace mvclibrary.ViewModels
{
    public class WalletViewModel
    {
        public wallet mywallet { get; set; }
        public List<walletTransaction> walletTransactionList { get; set; }
    }
}