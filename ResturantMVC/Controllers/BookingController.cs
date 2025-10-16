using Microsoft.AspNetCore.Mvc;
using ResturantMVC.Models;
using ResturantMVC.Services;

namespace ResturantMVC.Controllers
{
    public class BookingController : Controller
    {
        private readonly BookingApiService _bookingApi;

        public BookingController(BookingApiService bookingApi)
        {
            _bookingApi = bookingApi;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new BookingViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(BookingViewModel booking, string token)
        {
            if (!ModelState.IsValid)
            {
                return View(booking);
            }

            var success = await _bookingApi.CreateBookingAsync(booking, token);
            if (success)
            {
                TempData["Message"] = "Din bokning är mottagen!";
                return RedirectToAction("Confirmation");
            }
            ModelState.AddModelError("", "Något gick fel. Försök igen.");
            return View(booking);
        }

        public IActionResult Confirmation()
        {
            return View();
        }
    }
}
