using NAGPBank.CrossCutting.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using NAGPBank.CrossCutting.Types;
using NAGPBank.CrossCutting.Error;

namespace NAGPBank.Data.Repository
{
   public class TransactionsRepository
    {
        private BankDBContext bankDBContext;
        public TransactionsRepository(BankDBContext bankDBContext)
        {
            this.bankDBContext = bankDBContext;
        }

        public async Task Add(Transactions transactions)
        {
            var errorMessage = string.Empty;
            //var getAccount = bankDBContext.Account.FirstOrDefault(x => x.AccountNumber == transactions.AccountNumber && x.Status == AccountStatus.Active);
            if(transactions.Amount > 0)
            {
                if(transactions.TransactionMode == TransactionMode.Deposit)
                {
                    transactions.TransferToAccountNumber = string.Empty;
                    await bankDBContext.AddAsync<Transactions>(transactions);
                    await bankDBContext.SaveChangesAsync();
                }
                else if(transactions.TransactionMode == TransactionMode.Withdrawal)
                {
                    transactions.TransferToAccountNumber = string.Empty;
                    await bankDBContext.AddAsync<Transactions>(transactions);
                    await bankDBContext.SaveChangesAsync();
                }
                else if(transactions.TransactionMode == TransactionMode.Transfer)
                {
                    if (!string.IsNullOrEmpty(transactions.TransferToAccountNumber))
                    {
                        await bankDBContext.AddAsync<Transactions>(transactions);
                        await bankDBContext.SaveChangesAsync();
                    }
                    else
                    {
                        errorMessage = "Please provide payee account number.";
                    }
                }
                else
                {
                    errorMessage = "Invalid transaction mode.";
                }
            }
            else
            {
                errorMessage = "Transaction amount can not be less than 0.";
            }

            if(errorMessage != string.Empty)
            {
                throw new BankBaseException(errorMessage);
            }
        }

        public async Task<List<Transactions>> Get(string accountNumber)
        {
            var transactions = bankDBContext.Transactions.Where(x => x.AccountNumber == accountNumber).ToList();
            return transactions;
        }
    }
}
