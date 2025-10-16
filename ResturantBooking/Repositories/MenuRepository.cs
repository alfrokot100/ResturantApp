using Microsoft.EntityFrameworkCore;
using ResturantBooking.Data;
using ResturantBooking.Models;
using ResturantBooking.Repositories.IRepositories;

namespace ResturantBooking.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly AppDBContext _context ;

        // Injecerat
        public MenuRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<List<Menu>> GetAllMenusAsync()
        {
            var menus = await _context.Menus.ToListAsync();
            return menus;
        }

        public async Task<Menu> GetMenyByIDAsync(int menuID)
        {
            var menus = await _context.Menus.FirstOrDefaultAsync(m => m.Id == menuID);
            return menus;
        }
        public async Task<int> CreateMenyAsync(Menu Menu)
        {
            _context.Menus.Add(Menu);
            await _context.SaveChangesAsync();
            return Menu.Id;
        }
        public async Task<bool> UpdateMenuAsync(Menu Menu)
        {
            _context.Menus.Update(Menu);
            return await _context.SaveChangesAsync() > 0;
            //var result = await _context.SaveChangesAsync();
            //if(result != 0) { return true; }
            //return false;
        }
        public async Task<bool> DeleteMenuAsync(int MenuID)
        {
            var rowsAffected = await _context.Menus.Where(m => m.Id == MenuID).ExecuteDeleteAsync();
            if(rowsAffected > 0) { return true; }
            return false;
        }
    }
}
