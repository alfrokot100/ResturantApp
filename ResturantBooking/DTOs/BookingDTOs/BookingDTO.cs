namespace ResturantBooking.DTOs.BookingDTOs
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public int TableId_FK { get; set; }
        public int CustomerId_FK { get; set; }
        public int Guest { get; set; }
        public DateTime StartTime { get; set; }

        //Lite extra
        public string? Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int TableNumber { get; set; }
    }
}
