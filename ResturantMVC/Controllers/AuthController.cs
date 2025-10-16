using Microsoft.AspNetCore.Mvc;
using ResturantMVC.Models;
using ResturantMVC.Services;
using System.Text.Json;

namespace ResturantMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthApiService _apiService;

        public AuthController(AuthApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var token = await _apiService.LoginAsync(model);

            if (string.IsNullOrEmpty(token))
            {
                ModelState.AddModelError("", "Fel användarnamn eller lösenord.");
                return View(model);
            }

            HttpContext.Session.SetString("JWToken", token);
            return RedirectToAction("Index", "Home");

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JWToken");
            return RedirectToAction("Login");
        }
    }
}
