using System.Collections.Generic;
using System.Threading.Tasks;
using NAGPBank.CrossCutting.Dto;
using NAGPBank.CrossCutting.Models;
using NAGPBank.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> logger;
        private readonly CustomerRepository customerRepository;
        public CustomerController(CustomerRepository customerRepository, ILogger<CustomerController> logger)
        {
            this.customerRepository = customerRepository;
            this.logger = logger;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            this.logger.LogInformation("Testing");
            var customer = await customerRepository.GetById(id);
            if (customer != null)
            {
                return Ok(customer);
            }
            else
            {
                return NotFound(new ResponseDto("Customer id not found."));
            }
        }

        [HttpPost]
        public async Task<ResponseDto> AddCustomer(Customer customer)
        {
            var id = await customerRepository.Add(customer);
            return new ResponseDto($"Customer added successfully with id: {id}");
        }

        [HttpPut]
        public async Task<ResponseDto> UpdateCustomer(Customer customer)
        {
            await customerRepository.Update(customer);
            return new ResponseDto("Customer info updated successfully.");
        }
    }
}