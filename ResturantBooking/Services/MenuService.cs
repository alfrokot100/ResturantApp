using ResturantBooking.DTOs.MenuDTOs;
using ResturantBooking.Models;
using ResturantBooking.Repositories.IRepositories;
using ResturantBooking.Services.IServices;

namespace ResturantBooking.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepo;

        public MenuService(IMenuRepository menuRepo)
        {
            _menuRepo = menuRepo; 
        }
        public async Task<List<MenuDTO>> GetAllMenusAsync()
        {
            var menus = await _menuRepo.GetAllMenusAsync();
            var MenuDTO = menus.Select(m => new MenuDTO
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                IsPopular = m.IsPopular,
                Price = m.Price,
                ImageURL = m.ImageURL
            }).ToList();
            return MenuDTO;
        }

        public async Task<MenuDTO> GetMenyByIDAsync(int menuID)
        {
            var menu = await _menuRepo.GetMenyByIDAsync(menuID);
            if(menu == null) { return null; }

            var menuDTO = new MenuDTO
            {
                Id = menu.Id,
                Name = menu.Name,
                Description = menu.Description,
                IsPopular = menu.IsPopular,
                Price = menu.Price,
                ImageURL = menu.ImageURL
            };
            return menuDTO;
        }
        public async Task<int> CreateMenyAsync(MenuDTO menuDTO)
        {
            var menu = new Menu
            {
                Id = menuDTO.Id,
                Name = menuDTO.Name,
                Description = menuDTO.Description,
                IsPopular = menuDTO.IsPopular,
                Price = menuDTO.Price,
                ImageURL = menuDTO.ImageURL
            };
            var newMenuID = await _menuRepo.CreateMenyAsync(menu);
            return newMenuID;
        }
        public async Task<bool> UpdateMenuAsync(MenuDTO menuDTO)
        {
            var existingMenu = await _menuRepo.GetMenyByIDAsync(menuDTO.Id);
            if(existingMenu == null) { return false; }

            existingMenu.Name = menuDTO.Name;
            existingMenu.Description = menuDTO.Description;
            existingMenu.Price = menuDTO.Price;

            //var menu = new Menu
            //{
            //    Id = menuDTO.Id,
            //    Name = menuDTO.Name,
            //    Description = menuDTO.Description,
            //    IsPopular = menuDTO.IsPopular,
            //    Price = menuDTO.Price,
            //    ImageURL = menuDTO.ImageURL
            //};
            return await _menuRepo.UpdateMenuAsync(existingMenu);
        }
        public async Task<bool> DeleteMenuAsync(int MenuID)
        {
            return await _menuRepo.DeleteMenuAsync(MenuID);
        }

        public async Task<List<MenuDTO>> GetPopularMenusAsync(int count = 3)
        {
            var menus = await _menuRepo.GetAllMenusAsync();

            return menus.Take(count).Select(m => new MenuDTO
            {
                Id = m.Id,
                Name = m.Name,
                Price = m.Price,
                Description = m.Description,
            }).ToList();
        }
    }
}
