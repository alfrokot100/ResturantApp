using ResturantBooking.Models;

namespace ResturantBooking.Repositories.IRepositories
{
    public interface IAdminRepository
    {
        Task<Admin?> GetAdminByUsernameAsync(string username);
        Task<int> CreateAdminAsync(Admin admin);
    }
}
