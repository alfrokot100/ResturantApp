namespace ResturantMVC.Models
{
    public class BookingViewModel
    {
        public int TableId_FK { get; set; }
        public int Id { get; set; } // används om man hämtar bokningar
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public int Guest { get; set; }
        public string? Phone { get; set; }
    }
}
