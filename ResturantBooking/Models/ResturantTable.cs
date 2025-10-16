using System.ComponentModel.DataAnnotations;

namespace ResturantBooking.Models
{
    public class ResturantTable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, 200, ErrorMessage = "Bordsnummer måste vara mellan 1 och 200")]
        public int Number { get; set; }

        [Required]
        [Range(1, 20, ErrorMessage = "Kapacitet måste vara mellan 1 och 20")]
        public int Capacity { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
