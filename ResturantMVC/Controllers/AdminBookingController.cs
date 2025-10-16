using Microsoft.AspNetCore.Mvc;
using ResturantMVC.Models;
using ResturantMVC.Services;

namespace ResturantMVC.Controllers
{
    public class AdminBookingController : Controller
    {
        private readonly BookingApiService _bookingApi;

        public AdminBookingController(BookingApiService bookingApi)
        {
            _bookingApi = bookingApi;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            {
                // Om man inte är inloggad -> tillbaka till login
                return RedirectToAction("Login", "Auth");
            }
            var bookings = await _bookingApi.GetAllBookingsAsync(token);
            return View(bookings);
        }
        //Visar formulär för att skapa bokning
        [HttpGet]
        public IActionResult Create()
        {
            return View(new BookingViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookingViewModel booking)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token)) { return RedirectToAction("Login", "Auth"); }

            if (!ModelState.IsValid) { return View(booking); }

            var succes = await _bookingApi.CreateBookingAsync(booking, token);
            if (succes) { return RedirectToAction("Index"); }

            ModelState.AddModelError("", "Kunde inte skapa bokning.");
            return View(booking);
        }

        //Visar formulär för att redigera
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token)) { return RedirectToAction("Login", "Auth"); }

            var booking = await _bookingApi.GetBookingByIDAsync(id, token);
            if (booking == null) return NotFound();

            return View(booking);
        }

        //Uppdatera bokning
        [HttpPost]
        public async Task<IActionResult> Edit(BookingViewModel booking)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            { return RedirectToAction("Login", "Auth"); }

            if (!ModelState.IsValid)
            { return View(booking); }

            var succes = await _bookingApi.UpdateBookingAsync(booking, token);
            if (succes) { return RedirectToAction("Index"); }

            ModelState.AddModelError("", "Kunde inte uppdatera bokning.");
            return View(booking);
        }

        //Detaljer om bokningen
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var booking = await _bookingApi.GetBookingByIDAsync(id, token);
            if (booking == null)
                return NotFound();

            return View(booking);
        }


        // Ta bort bokning
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var booking = await _bookingApi.GetBookingByIDAsync(id, token);
            if (booking == null) return NotFound();

            return View(booking);
        }

        [HttpPost,ActionName("Confirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token)) { return RedirectToAction("Login", "Auth"); }

            var success = await _bookingApi.DeleteBookingAsync(id, token);
            return RedirectToAction("Index");
        }
    }
}
