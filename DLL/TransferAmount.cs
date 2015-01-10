using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DLL
{
    public class TransferAmount
    {
        private offcampus4uEntities db;


        public Error WithdrawAmount(Int64 userId)
        {
            db = new offcampus4uEntities();
            Error objError = new Error();
            try
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        wallet objWallet = db.wallets.Where(x => x.accountId.Equals(userId)).FirstOrDefault();
                        if (objWallet != null)
                        {
                            walletTransaction objWalletTransaction = new walletTransaction();
                            objWalletTransaction.walletId = objWallet.walletId;
                            objWalletTransaction.walletDescription = "Transfer Request from User";
                            objWalletTransaction.transactionAmount = objWallet.walletBalance;
                            objWalletTransaction.createdBy = userId;
                            objWalletTransaction.createdOn = DateTime.Now;
                            objWalletTransaction.modifiedBy = userId;
                            objWalletTransaction.modifiedOn = DateTime.Now;
                            objWalletTransaction.transactionStatus = 1;
                            objWalletTransaction.isDeleted = false;
                            db.walletTransactions.Add(objWalletTransaction);
                            db.SaveChanges();
                            withdrawal objWithdrawal = new withdrawal();
                            objWithdrawal.accountId = userId;
                            objWithdrawal.walletId = objWallet.walletId;
                            objWithdrawal.walletBalance = objWallet.walletBalance;
                            objWithdrawal.createdBy = userId;
                            objWithdrawal.createdOn = DateTime.Now;
                            objWithdrawal.modifiedBy = userId;
                            objWithdrawal.modifiedOn = DateTime.Now;
                            objWithdrawal.isProcessed = true;
                            db.withdrawals.Add(objWithdrawal);
                            db.SaveChanges();
                            objWallet.walletBalance = "0";
                            objWallet.modifiedBy = userId;
                            objWallet.modifiedOn = DateTime.Now;
                            db.SaveChanges();
                            
                        }
                        transaction.Complete();
                        objError.isSuccess = true;
                        objError.message = "Operation successfully performed.";
                    }
                    catch(Exception ex)
                    {
                        transaction.Dispose();
                    }
                }
            }
            catch
            {
                objError.isSuccess = false;
                objError.message = "Oops.. something went wrong. please try again later.";
            }
            return objError;
        }
    }
}
