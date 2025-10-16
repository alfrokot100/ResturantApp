using System.ComponentModel.DataAnnotations;

namespace ResturantBooking.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Användarnamnet kan vara max 50 tecken")]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }
}
