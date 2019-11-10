using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NAGPBank.CrossCutting.Dto;
using NAGPBank.CrossCutting.Models;
using NAGPBank.Data.Repository;

namespace TransactionAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly TransactionsRepository transactionsRepository;
        public TransactionsController(TransactionsRepository transactionsRepository)
        {
            this.transactionsRepository = transactionsRepository;
        }

        [HttpGet]
        [Route("{accountNumber}")]
        public async Task<List<Transactions>> GetTransactions(string accountNumber)
        {
            var transactions = await transactionsRepository.Get(accountNumber);
            return transactions;
        }

        [HttpPost]
        public async Task<ResponseDto> AddTransactions(Transactions transactions)
        {
            await transactionsRepository.Add(transactions);
            return new ResponseDto($"Transaction was successfull.");
        }
    }
}