using ResturantBooking.Models;

namespace ResturantBooking.Repositories.IRepositories
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIDAsync(int customerID);
        Task<Customer> GetCustomerByEmailAsync(string email);
        Task<int> CreateCustomerAsync(Customer Customer);
        Task<bool> UpdateCustomerAsync(Customer Customer);
        Task<bool> DeleteCustomerAsync(int customerID);
    }
}
