using NAGPBank.CrossCutting.Models;
using NAGPBank.CrossCutting.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using NAGPBank.CrossCutting.Dto;
using NAGPBank.CrossCutting.Helpers;
using NAGPBank.CrossCutting.Error;

namespace NAGPBank.Data.Repository
{
    public class AccountRepository
    {
        private BankDBContext bankDBContext;
        public AccountRepository(BankDBContext bankDBContext)
        {
            this.bankDBContext = bankDBContext;
        }

        public async Task<Account> GetById(int id)
        {
            return await bankDBContext.FindAsync<Account>(id);
        }

        public async Task<string> Add(Account account)
        {
            account.AccountNumber = RandomNumberHelper.GenerateNumber();
            await bankDBContext.AddAsync<Account>(account);
            await bankDBContext.SaveChangesAsync();
            return account.AccountNumber;
        }

        public async Task UpdateAccountStatus(string accountNumber, AccountStatus accountStatus)
        {
            var getAccount = bankDBContext.Account.FirstOrDefault(x => x.AccountNumber == accountNumber);
            if (getAccount != null)
            {
                getAccount.Status = accountStatus;
                bankDBContext.Update<Account>(getAccount);
                await bankDBContext.SaveChangesAsync();
            }
            else
            {
                throw new NotFoundException("Account not found.");
            }
        }

        public List<Account> GetAccountsByUserId(int id)
        {
            var accounts = bankDBContext.Account.Where(x => x.CustomerId == id && x.Status == AccountStatus.Active).ToList();
            return accounts;
        }

        public async Task TransferAccount(TransferAccountDto transferAccountDto)
        {
            var getAccount = bankDBContext.Account.FirstOrDefault(x => x.AccountNumber == transferAccountDto.AccountNumber);
            if(getAccount != null)
            {
                if(getAccount.Status != AccountStatus.Closed)
                {
                    getAccount.BranchName = transferAccountDto.BranchName;
                    getAccount.IFSC = transferAccountDto.IFSC;
                    bankDBContext.Update<Account>(getAccount);
                    await bankDBContext.SaveChangesAsync();
                }
                else
                {
                    throw new BankBaseException("Closed account can not be transfered.");
                }
            }
            else
            {
                throw new NotFoundException("Account not found.");
            }
        }

    }
}
