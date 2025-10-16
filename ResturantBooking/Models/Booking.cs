using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResturantBooking.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, 20, ErrorMessage = "Antalet gäster måste vara mellan 1 och 20")]
        public int Guest { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [ForeignKey("Table")]
        public int TableId_FK { get; set; }
        public ResturantTable Table { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId_FK { get; set; }
        public Customer Customer { get; set; }
    }
}
