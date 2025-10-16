namespace ResturantMVC.Models
{
    public class HomeViewModel
    {
        public string RestaurantName { get; set; }
        public string Description { get; set; }
        public List<DishViewModel> PopularDishes { get; set; } = new();
    }
    //public class DishViewModel
    //{
    //    public string Name { get; set; }
    //    public string Description { get; set; }
    //    public decimal Price { get; set; }
    //    public string? BildUrl { get; set; }
    //}
}
