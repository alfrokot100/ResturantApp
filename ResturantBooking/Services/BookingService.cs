using ResturantBooking.DTOs.BookingDTOs;
using ResturantBooking.DTOs.TableDTOs;
using ResturantBooking.Models;
using ResturantBooking.Repositories.IRepositories;
using ResturantBooking.Services.IServices;

namespace ResturantBooking.Services
{
    public class BookingService : IBookingService
    {
        private readonly ITableRepository _tableRepo;
        private readonly IBookingRepository _bookingRepo;
        private readonly ICustomerRepository _customerRepo;

        public BookingService(ITableRepository tableRepo, IBookingRepository bookingRepo, ICustomerRepository customerRepo)
        {
            _tableRepo = tableRepo;
            _bookingRepo = bookingRepo;
            _customerRepo = customerRepo;
        }

        //Hämtar alla bokningar
        public async Task<List<BookingDTO>> GetAllBookingsAsync()
        {
            var bookings = await _bookingRepo.GetAllBookingsAsync();
            return bookings.Select(b => new BookingDTO
            {
                Id = b.Id,
                TableId_FK = b.TableId_FK,
                CustomerId_FK = b.CustomerId_FK,
                Guest = b.Guest,
                StartTime = b.StartTime,
                Name = b.Customer?.Name,
                TableNumber = b.Table?.Number ?? 0,
                Email = b.Customer.Email,
                Phone = b.Customer.Phone
            }).ToList();
        }
        
        public async Task<BookingDTO?> GetBookingByIdAsync(int id)
        {
            var booking = await _bookingRepo.GetBookingByIdAsync(id);
            if (booking == null) return null;

            return new BookingDTO
            {
                Id = booking.Id,
                TableId_FK = booking.TableId_FK,
                CustomerId_FK = booking.CustomerId_FK,
                Guest = booking.Guest,
                StartTime = booking.StartTime,
                Email = booking.Customer.Email,
                Phone = booking.Customer.Phone,
                Name = booking.Customer.Name
            };
        }
        //Skapar en bokning
        public async Task<BookingDTO?> CreateBookingAsync(BookingCreateDTO dto)
        {
            var existingBookings = await _bookingRepo.GetBookingsByTableAsync(dto.TableId_FK);

            bool conflict = false; 

            if (conflict) { return null; }

            // Hämta/skapar kund
            int customerId;
            Customer? existingCustomer = null;

            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                existingCustomer = await _customerRepo.GetCustomerByEmailAsync(dto.Email);
            }

            if (existingCustomer != null)
            {
                if(existingCustomer.Name != dto.Name || existingCustomer.Phone != dto.Phone)
                {
                    existingCustomer.Name = dto.Name;
                    existingCustomer.Phone = dto.Phone;
                    await _customerRepo.UpdateCustomerAsync(existingCustomer);
                }
               
                Console.WriteLine($"[BookingService] E-post: {dto.Email}, hittad kund: {existingCustomer?.Name ?? "ingen"}");
                customerId = existingCustomer.Id; // använd befintlig kund
            }
            else
            {
                var newCustomer = new Customer
                {
                    Name = dto.Name,
                    Phone = dto.Phone,
                    Email = dto.Email
                };
                customerId = await _customerRepo.CreateCustomerAsync(newCustomer);
                Console.WriteLine($"[BookingService] Skapar kund: {dto.Name}, {dto.Email}, {dto.Phone}");

            }

            //Skapa bokning
            var booking = new Booking
            {
                TableId_FK = dto.TableId_FK,
                CustomerId_FK = customerId,
                Guest = dto.Guest,
                StartTime = dto.StartTime
            };
            var bookingId = await _bookingRepo.CreateBookingAsync(booking);
            var createdBooking = await _bookingRepo.GetBookingByIdAsync(bookingId); //Bokningen sparas via repo

            if (createdBooking == null) { return null; }
            Console.WriteLine($"[BookingService] Skapar bokning: Kund={dto.Name}, Bord={dto.TableId_FK}, Gäster={dto.Guest}, Tid={dto.StartTime}");


            //Returnera DTO
            return new BookingDTO
            {
                Id = createdBooking.Id,
                TableId_FK = createdBooking.TableId_FK,
                CustomerId_FK = createdBooking.CustomerId_FK,
                Guest = createdBooking.Guest,
                StartTime = createdBooking.StartTime,
                Name = createdBooking.Customer?.Name,
                Email = createdBooking.Customer?.Email,
                Phone = createdBooking.Customer?.Phone,
                TableNumber = createdBooking.Table?.Number ?? 0
            };

        }

        //Uppdatera bokning
        public async Task<bool> UpdateBookingAsync(BookingUpdateDTO bookingDto)
        {
            var existingBooking = await _bookingRepo.GetBookingByIdAsync(bookingDto.Id);
            if (existingBooking == null)
                return false;
            existingBooking.TableId_FK = bookingDto.TableId_FK;
            existingBooking.Guest = bookingDto.Guest;
            existingBooking.StartTime = bookingDto.StartTime;

            if (existingBooking.Customer != null)
            {
                existingBooking.Customer.Name = bookingDto.Name;
                existingBooking.Customer.Email = bookingDto.Email;
                existingBooking.Customer.Phone = bookingDto.Phone;

                await _customerRepo.UpdateCustomerAsync(existingBooking.Customer);
            }
            return await _bookingRepo.UpdateBookingAsync(existingBooking);
        }
        public async Task<bool> DeleteBookingAsync(int id)
        {
            return await _bookingRepo.DeleteBookingAsync(id);
        }

        // Hämtar lediga bord via repository
        public async Task<List<TableDTO>> GetAvailableTablesAsync(DateTime startTime, int guests)
        {
            //Länken mellan frontend och databas
            return await _bookingRepo.GetAvailableTablesAsync(startTime, guests);
        }
    }
}
