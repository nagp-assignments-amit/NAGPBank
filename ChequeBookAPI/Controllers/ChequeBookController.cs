using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NAGPBank.CrossCutting.Dto;
using NAGPBank.CrossCutting.Models;
using NAGPBank.Data.Repository;

namespace ChequeBookAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChequeBookController : ControllerBase
    {
        private readonly ChequeBookRepository chequeBookRepository;
        public ChequeBookController(ChequeBookRepository chequeBookRepository)
        {
            this.chequeBookRepository = chequeBookRepository;
        }

        [Route("{customerId}")]
        [HttpGet]
        public List<ChequeBook> GetChequeBook(int customerId)
        {
            var data = chequeBookRepository.GetChequeBookByCustomer(customerId);
            return data;
        }

        [HttpPost]
        public async Task<ResponseDto> IssueChequeBook(ChequeBook chequeBook)
        {
            var chequeBookNumber = await chequeBookRepository.IssueChequeBook(chequeBook);
            return new ResponseDto($"ChequeBook has been issued with number: {chequeBookNumber}");
        }

        [Route("{chequeBookNumber}")]
        [HttpPut]
        public async Task<ResponseDto> BlockChequeBook(string chequeBookNumber)
        {
            await chequeBookRepository.BlockChequeBook(chequeBookNumber);
            return new ResponseDto("ChequeBook blocked successfully.");
        }
    }
}