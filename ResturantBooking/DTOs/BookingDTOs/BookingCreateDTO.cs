namespace ResturantBooking.DTOs.BookingDTOs
{
    public class BookingCreateDTO
    {
        public int TableId_FK { get; set; }
        public int? CustomerId_FK { get; set; }
        public int Guest { get; set; }
        public DateTime StartTime { get; set; }

        // Kunddata
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
    }
}
