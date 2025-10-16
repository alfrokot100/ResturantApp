using ResturantBooking.DTOs.MenuDTOs;
using ResturantBooking.Models;

namespace ResturantBooking.Services.IServices
{
    public interface IMenuService
    {
        Task<List<MenuDTO>> GetAllMenusAsync();
        Task<MenuDTO> GetMenyByIDAsync(int menuID);
        Task<int> CreateMenyAsync(MenuDTO menuDTO);
        Task<bool> UpdateMenuAsync(MenuDTO menuDTO);
        Task<bool> DeleteMenuAsync(int MenuID);
        //Popular menus
        Task<List<MenuDTO>> GetPopularMenusAsync(int count = 3);

    }
}

