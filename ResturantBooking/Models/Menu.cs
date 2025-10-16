using System.ComponentModel.DataAnnotations;

namespace ResturantBooking.Models
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Namnet kan vara max 100 tecken")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Beskrivningen kan vara max 500 tecken")]
        public string Description { get; set; }

        [Range(0.01, 10000, ErrorMessage = "Priset måste vara större än 0")]
        public Decimal Price { get; set; }
        public bool IsPopular { get; set; }

        [Url(ErrorMessage = "BildURL måste vara en giltig webbadress")]
        public string? ImageURL { get; set; }
    }
}
