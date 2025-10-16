using Microsoft.EntityFrameworkCore;
using ResturantBooking.Data;
using ResturantBooking.Models;
using ResturantBooking.Repositories.IRepositories;

namespace ResturantBooking.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDBContext _context;

        public AdminRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAdminAsync(Admin admin)
        {
            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();
            return admin.Id;
        }

        public async Task<Admin?> GetAdminByUsernameAsync(string username)
        {
            return await _context.Admins.FirstOrDefaultAsync(a => a.Username == username);

        }
    }
}
