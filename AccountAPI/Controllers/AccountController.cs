using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NAGPBank.CrossCutting.Dto;
using NAGPBank.CrossCutting.Models;
using NAGPBank.CrossCutting.Types;
using NAGPBank.Data.Repository;

namespace AccountAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountRepository accountRepository;
        public AccountController(AccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        [HttpPost]
        public async Task<ResponseDto> CreateAccount(Account account)
        {
           var accountNumber = await accountRepository.Add(account);
            return new ResponseDto($"Account created successfully with account number: {accountNumber}");
        }

        [Route("{id}")]
        public ActionResult<List<Account>> GetUserAccounts(int id)
        {
            var accounts = accountRepository.GetAccountsByUserId(id);
            return accounts;
        }

        [HttpPost]
        public async Task<ResponseDto> TransferAccount(TransferAccountDto transferAccountDto)
        {
            await accountRepository.TransferAccount(transferAccountDto);
            return new ResponseDto("Account transfered successfully.");
        }

        [HttpPut]
        [Route("{accountNumber}")]
        public async Task<ResponseDto> CloseAccount(string accountNumber)
        {
            await accountRepository.UpdateAccountStatus(accountNumber, AccountStatus.Closed);
            return new ResponseDto("Account closed successfully.");
        }
    }
}