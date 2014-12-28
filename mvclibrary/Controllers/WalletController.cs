using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLL;
using mvclibrary.ViewModels;


namespace mvclibrary.Controllers
{
    public class WalletController : Controller
    {
        //
        // GET: /Wallet/

        public ActionResult Index()
        {
            WalletViewModel objWalletViewModel = new WalletViewModel();
            MyWallet objMywallet = new MyWallet();
            objWalletViewModel.mywallet = objMywallet.getmyWallet(Convert.ToInt64(User.Identity.Name));
            if (objWalletViewModel.mywallet != null)
            {
                objWalletViewModel.walletTransactionList = objMywallet.getWalletTransactions(objWalletViewModel.mywallet.walletId);
            }
            return View(objWalletViewModel);
        }

        public ActionResult BankDetail()
        {
            Profile objProfile = new Profile();
            BankDetail objBankDetail = objProfile.getBankDetail(Convert.ToInt64(User.Identity.Name));
            BankViewModel objBankViewModel = new BankViewModel();
            if (objBankDetail != null)
            {
                objBankViewModel.accountholdername = objBankDetail.BankAccountName == null ? "" : objBankDetail.BankAccountName;
                objBankViewModel.accountifsccode = objBankDetail.BankIFSCCode == null ? "" : objBankDetail.BankIFSCCode;
                objBankViewModel.accountnumber = objBankDetail.BankAccountNumber == null ? "" : objBankDetail.BankAccountNumber;
            }
            return View(objBankViewModel);
        }

        public JsonResult updateBank(string accountnumber, string holdername, string ifsccode){
            Error objError = new Error();
            if (string.IsNullOrEmpty(accountnumber))
            {
                objError.isSuccess = false;
                objError.message = "Account Number can't be empty.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(holdername))
            {
                objError.isSuccess = false;
                objError.message = "Account Holder Name can't be empty.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(accountnumber))
            {
                objError.isSuccess = false;
                objError.message = "Branch IFSC Code can't be empty.";
                return Json(objError, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Profile objProfile = new Profile();
                BankDetail objBankdetail = new DLL.BankDetail();
                objBankdetail.BankAccountName = holdername;
                objBankdetail.BankAccountNumber = accountnumber;
                objBankdetail.BankIFSCCode = ifsccode;
                objBankdetail.accountId = Convert.ToInt64(User.Identity.Name);
                objBankdetail.createdBy = Convert.ToInt64(User.Identity.Name);
                objBankdetail.createdOn = DateTime.Now;
                objBankdetail.modifiedBy = Convert.ToInt64(User.Identity.Name);
                objBankdetail.modifiedOn = DateTime.Now;
                objBankdetail.isDeleted = false;
                objError = objProfile.updateBankDetail(objBankdetail);
                return Json(objError, JsonRequestBehavior.AllowGet);
            }
        }

       

    }
}
