using Microsoft.AspNetCore.Mvc;
using ResturantMVC.Models;
using ResturantMVC.Services;

namespace ResturantMVC.Controllers
{
    public class MenuController : Controller
    {
        private readonly MenuApiService _menuApi;

        public MenuController(MenuApiService menuApi)
        {
            _menuApi = menuApi;
        }

        public async Task<IActionResult> Index()
        {
            var dishes = await _menuApi.GetAllDishesAsync();
            var model = new MenuViewModel
            {
                Dishes = dishes
            };
            return View(model);
        }

    }
}
