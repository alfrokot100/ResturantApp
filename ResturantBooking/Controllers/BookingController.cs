using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResturantBooking.DTOs.BookingDTOs;
using ResturantBooking.Services.IServices;

namespace ResturantBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllBookings() //Hämtar alla bokningar - endast admin
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null) return NotFound();
            return Ok(booking);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateBooking([FromBody] BookingCreateDTO bookingDTO)
        {
            var result = await _bookingService.CreateBookingAsync(bookingDTO);
            if (result == null)
                return BadRequest("Bordet är inte tillgängligt.");
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBooking(int id, BookingUpdateDTO dto)
        {
            if (id != dto.Id) return BadRequest("Id mismatch");

            var success = await _bookingService.UpdateBookingAsync(dto);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var success = await _bookingService.DeleteBookingAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }

        //Letar efter lediga bord vid en viss tidpunkt
        [HttpGet("available")]
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAvailableTables(DateTime startTime, int guests)
        {
            var tables = await _bookingService.GetAvailableTablesAsync(startTime, guests);
            return Ok(tables);
        }
    }
}
