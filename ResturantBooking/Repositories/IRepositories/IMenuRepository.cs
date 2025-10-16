using ResturantBooking.Models;

namespace ResturantBooking.Repositories.IRepositories
{
    public interface IMenuRepository
    {
        Task<List<Menu>> GetAllMenusAsync();
        Task<Menu> GetMenyByIDAsync(int menuID);
        Task<int> CreateMenyAsync(Menu Menu);
        Task<bool> UpdateMenuAsync(Menu Menu);
        Task<bool> DeleteMenuAsync(int MenuID);
    }
}
