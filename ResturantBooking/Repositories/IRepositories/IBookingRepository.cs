using ResturantBooking.Models;

namespace ResturantBooking.Repositories.IRepositories
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetAllBookingsAsync();
        Task<Booking?> GetBookingByIdAsync(int id);
        Task<int> CreateBookingAsync(Booking booking);
        Task<bool> UpdateBookingAsync(Booking booking);
        Task<bool> DeleteBookingAsync(int id);
        Task<List<Booking>> GetBookingsByTableAsync(int tableId);

        //Extra
        Task<List<ResturantTable>> GetAvailableTablesAsync(DateTime startTime, int guests);

    }
}
