using ResturantBooking.DTOs.CustomerDTOs;
using ResturantBooking.Models;

namespace ResturantBooking.Services.IServices
{
    public interface ICustomerService
    {
        Task<List<CustomerDTO>> GetAllCustomersAsync();
        Task<CustomerDTO> GetCustomerByID(int CustomerID);
        Task<int> CreateCustomerAsync(CustomerDTO customerDTO);
        Task<Customer?> GetCustomerByEmailAsync(string email);
        Task<bool> UpdateCustomerAsync(CustomerDTO customerDTO);
        Task<bool> DeleteCustomerAsync(int CustomerID);
    }
}
