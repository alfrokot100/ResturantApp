using ResturantBooking.DTOs.BookingDTOs;
using ResturantBooking.DTOs.TableDTOs;
using ResturantBooking.Models;

namespace ResturantBooking.Services.IServices
{
    public interface IBookingService
    {
        Task<List<BookingDTO>> GetAllBookingsAsync();
        Task<BookingDTO?> GetBookingByIdAsync(int id);
        Task<BookingDTO?> CreateBookingAsync(BookingCreateDTO bookingDto);
        Task<bool> UpdateBookingAsync(BookingUpdateDTO bookingDto);
        Task<bool> DeleteBookingAsync(int id);
        
        // Extra för kravet: lediga bord
        Task<List<TableDTO>> GetAvailableTablesAsync(DateTime startTime, int guests);
    }
}

