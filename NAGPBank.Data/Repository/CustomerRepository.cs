using NAGPBank.CrossCutting.Error;
using NAGPBank.CrossCutting.Models;
using System;
using System.Threading.Tasks;

namespace NAGPBank.Data.Repository
{
    public class CustomerRepository
    {
        private BankDBContext bankDBContext;
        public CustomerRepository(BankDBContext bankDBContext)
        {
            this.bankDBContext = bankDBContext;
        }

        public async Task<int> Add(Customer customer )
        {
            await bankDBContext.AddAsync<Customer>(customer);
            await bankDBContext.SaveChangesAsync();
            return customer.Id;
        }

        public async Task<Customer> GetById(int id)
        {
            return await bankDBContext.FindAsync<Customer>(id);
        }

        public async Task Update(Customer customer)
        {
            var getCustomer = await bankDBContext.FindAsync<Customer>(customer.Id);
            if(getCustomer != null)
            {
                getCustomer.FatherName = customer.FatherName;
                getCustomer.Mobile = customer.Mobile;
                getCustomer.Name = customer.Name;
                getCustomer.Email = customer.Email;
                getCustomer.Aadhar = customer.Aadhar;
                bankDBContext.Update<Customer>(getCustomer);
                await bankDBContext.SaveChangesAsync();
            }
            else
            {
                throw new NotFoundException("Customer not found.");
            }
        }
    }
}
