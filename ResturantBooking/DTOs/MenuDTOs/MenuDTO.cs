namespace ResturantBooking.DTOs.MenuDTOs
{
    public class MenuDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public bool IsPopular { get; set; }
        public string? ImageURL { get; set; }

    }
}
