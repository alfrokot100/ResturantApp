using Microsoft.AspNetCore.Mvc;
using ResturantMVC.Models;
using ResturantMVC.Services;
using System.Diagnostics;

namespace ResturantMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly MenuApiService _menuApi;

        public HomeController(MenuApiService menuApi)
        {
            _menuApi = menuApi;
        }

        public async Task<IActionResult> Index()
        {
            var popularDishes = await _menuApi.GetPopularDishesAsync();

            var viewModel = new HomeViewModel
            {
                RestaurantName = "La Tavola Rustica",
                Description = "En mysig italiensk restaurang med fokus på hemlagad pasta och vedugnsbakad pizza.",
                PopularDishes = popularDishes
            };
            return View(viewModel);

        }

    }
}
