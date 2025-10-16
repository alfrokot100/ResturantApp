namespace ResturantMVC.Models
{
    public class DishViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public bool IsPopular { get; set; }
    }
}
