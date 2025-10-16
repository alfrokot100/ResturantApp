using ResturantBooking.Models;

namespace ResturantBooking.Services.IServices
{
    public interface IAdminService
    {
        Task<string?> LoginAsync(string username, string password);
        Task<int> CreateAdminAsync(string username, string password);
    }
}
