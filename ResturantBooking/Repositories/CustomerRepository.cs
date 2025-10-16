using Microsoft.EntityFrameworkCore;
using ResturantBooking.Data;
using ResturantBooking.Models;
using ResturantBooking.Repositories.IRepositories;

namespace ResturantBooking.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDBContext _context;

        public CustomerRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            var customers = await _context.Customers.ToListAsync();
            return customers;
        }

        public async Task<Customer?> GetCustomerByIDAsync(int customerID)
        {
                return await _context.Customers
            .Include(c => c.Bookings)
            .FirstOrDefaultAsync(c => c.Id == customerID);
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            //_context.Customers.Update(Customer);
            //var result = await _context.SaveChangesAsync();
            //if(result != 0) { return true; }
            //return false;

            var existing = await _context.Customers.FindAsync(customer.Id);

            if (existing == null) return false;

            existing.Name = customer.Name;
            existing.Phone = customer.Phone;
            existing.Email = customer.Email;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Customer?> GetCustomerByEmailAsync(string email)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<int> CreateCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer.Id;
        }
        public async Task<bool> DeleteCustomerAsync(int customerID)
        {
            var rowsAffected = await _context.Customers.Where(c => c.Id == customerID).ExecuteDeleteAsync();
            if(rowsAffected > 0) { return true; }
            return false;
        }


    }
}
