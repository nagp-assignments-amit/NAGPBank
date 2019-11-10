using NAGPBank.CrossCutting.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using NAGPBank.CrossCutting.Helpers;
using NAGPBank.CrossCutting.Types;
using NAGPBank.CrossCutting.Error;

namespace NAGPBank.Data.Repository
{
    public class ChequeBookRepository
    {
        private BankDBContext bankDBContext;
        public ChequeBookRepository(BankDBContext bankDBContext)
        {
            this.bankDBContext = bankDBContext;
        }

        public List<ChequeBook> GetChequeBookByCustomer(int customerId)
        {
            var data = bankDBContext.ChequeBook.Where(x => x.CustomerId == customerId).ToList();
            return data;
        }

        public async Task<string> IssueChequeBook(ChequeBook chequeBook)
        {
            chequeBook.ChequeBookNumber = RandomNumberHelper.GenerateNumber();
            await bankDBContext.AddAsync<ChequeBook>(chequeBook);
            await bankDBContext.SaveChangesAsync();
            return chequeBook.ChequeBookNumber;
        }

        public async Task BlockChequeBook(string chequeBookNumber)
        {
            var data = bankDBContext.ChequeBook.FirstOrDefault(x => x.ChequeBookNumber == chequeBookNumber);
            if(data != null)
            {
                data.Status = ChequeBookStatus.Blocked;
                await bankDBContext.SaveChangesAsync();
            }
            else
            {
                throw new NotFoundException("Cheque Book not found.");
            }
        }
    }
}
