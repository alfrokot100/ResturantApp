using System.ComponentModel.DataAnnotations;

namespace ResturantBooking.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Namnet kan vara max 100 tecken")]
        public string Name { get; set; }

        [StringLength(20)]
        public string? Phone { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; } = string.Empty;
        public List<Booking> Bookings  { get; set; }
    }
}
