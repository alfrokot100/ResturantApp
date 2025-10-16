using ResturantBooking.DTOs.BookingDTOs;
using ResturantBooking.Models;
using ResturantMVC.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ResturantMVC.Services
{
    public class BookingApiService
    {
        private readonly HttpClient _httpClient;

        public BookingApiService(HttpClient httpClient )
        {
            _httpClient = httpClient;
        }

        private void AddJwtHeader(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
        }

        //Task för att 
        public async Task<List<BookingViewModel>> GetAllBookingsAsync(string token)
        {
            AddJwtHeader(token);
            var booking = await _httpClient.GetFromJsonAsync<List<BookingViewModel>>("api/Booking");
            return booking ?? new List<BookingViewModel>();
        }

        public async Task<bool> CreateBookingAsync(BookingViewModel booking, string token)
        {
            AddJwtHeader(token);

            var dto = new BookingCreateDTO
            {
                TableId_FK = booking.TableId_FK, //Välj bord
                Guest = booking.Guest,
                StartTime = booking.StartTime,
                Name = booking.Name,
                Email = booking.Email, //Email 
                Phone = booking.Phone
            };

            if (dto.StartTime == DateTime.MinValue)
            { throw new ArgumentException("StartTime måste anges."); }
            
            var json = System.Text.Json.JsonSerializer.Serialize(dto);
            Console.WriteLine("REQUEST BODY: " + json);

            var response = await _httpClient.PostAsJsonAsync("api/Booking", dto);
            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine("RESPONSE: " + responseBody);

            return response.IsSuccessStatusCode;
        }

        public async Task<BookingViewModel> GetBookingByIDAsync(int id, string token)
        {
            AddJwtHeader(token);
            var response = await _httpClient.GetAsync($"api/Booking/{id}");
            if (!response.IsSuccessStatusCode) { return null; }

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<BookingViewModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        //Uppdaterar bokningen på sidan
        public async Task<bool> UpdateBookingAsync(BookingViewModel booking, string token)
        {
            AddJwtHeader(token);
            var response = await _httpClient.PutAsJsonAsync($"api/Booking/{booking.Id}", booking);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteBookingAsync(int id, string token)
        {
            AddJwtHeader(token);
            var response = await _httpClient.DeleteAsync($"api/Booking/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
