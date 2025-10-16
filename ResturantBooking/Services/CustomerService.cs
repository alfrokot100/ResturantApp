using ResturantBooking.DTOs.CustomerDTOs;
using ResturantBooking.Models;
using ResturantBooking.Repositories.IRepositories;
using ResturantBooking.Services.IServices;

namespace ResturantBooking.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepo;

        public CustomerService(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public async Task<List<CustomerDTO>> GetAllCustomersAsync()
        {
            var customers = await _customerRepo.GetAllCustomersAsync();
            var customerDTO = customers.Select(c => new CustomerDTO
            {
                Id = c.Id,
                Name = c.Name,
                Phone = c.Phone,
                Email = c.Email
            }).ToList();
            return customerDTO;
        }

        public async Task<CustomerDTO> GetCustomerByID(int CustomerID)
        {
            var customer = await _customerRepo.GetCustomerByIDAsync(CustomerID);
            if(customer == null) { return null; }

            var customerDTO = new CustomerDTO
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Email = customer.Email
            };
            return customerDTO;
        }

        public async Task<Customer?> GetCustomerByEmailAsync(string email)
        {
            return await _customerRepo.GetCustomerByEmailAsync(email);
        }

        public async Task<int> CreateCustomerAsync(CustomerDTO customerDTO)
        {
            var customer = new Customer
            {
                Id = customerDTO.Id,
                Name = customerDTO.Name,
                Phone = customerDTO.Phone,
                Email = customerDTO.Email
                
            };
            var newCustomerId = await _customerRepo.CreateCustomerAsync(customer);
            return newCustomerId;
        }

        public async Task<bool> UpdateCustomerAsync(CustomerDTO customerDTO)
        {
            var customer = new Customer
            {
                Id = customerDTO.Id,
                Name = customerDTO.Name,
                Phone = customerDTO.Phone,
                Email = customerDTO.Email
            };
            return await _customerRepo.UpdateCustomerAsync(customer);
        }

        public async Task<bool> DeleteCustomerAsync(int CustomerID)
        {
            return await _customerRepo.DeleteCustomerAsync(CustomerID);
        }
    }
}
