using Microsoft.AspNetCore.Mvc;
using ResturantMVC.Models;
using ResturantMVC.Services;

namespace ResturantMVC.Controllers
{
    public class AdminMenuController : Controller
    {
        private readonly MenuApiService _menuApi;

        public AdminMenuController(MenuApiService menuApi)
        {
            _menuApi = menuApi;
        }
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var dishes = await _menuApi.GetAllDishesAsync(token);

            return View(dishes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new DishViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(DishViewModel dish)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            { return RedirectToAction("Login", "Auth"); }

            if (!ModelState.IsValid) { return View(dish); }

            var succes = await _menuApi.CreateDishAsync(dish, token);
            if(succes) { return RedirectToAction("Index"); }

            ModelState.AddModelError("", "Kunde inte skapa rätt.");
            return View(dish);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            { return RedirectToAction("Login", "Auth"); }

            var dish = await _menuApi.GetDishByIdAsync(id, token);
            if(dish == null) { return NotFound(); }
            return View(dish);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DishViewModel dish)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            { return RedirectToAction("Login", "Auth"); }

            var success = await _menuApi.UpdateDishAsync(dish, token);
            if (success) return RedirectToAction("Index");

            ModelState.AddModelError("", "Kunde inte uppdatera rätt.");
            return View(dish);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            { return RedirectToAction("Login", "Auth"); }

            var dish = await _menuApi.GetDishByIdAsync(id, token);
            if (dish == null) return NotFound();
            return View(dish);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            { return RedirectToAction("Login", "Auth"); }

            var succes = await _menuApi.DeleteDishAsync(token, id);
            if (succes) { return RedirectToAction("Index"); }

            ModelState.AddModelError("","Kunde inte ta bort rätten");
            return View();
        }
    }
}
