using Microsoft.EntityFrameworkCore;
using ResturantBooking.Models;

namespace ResturantBooking.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            
        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<ResturantTable> Tables { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var tableEntity = modelBuilder.Entity<ResturantTable>();
            tableEntity.ToTable("ResturantTables");
            var customerEntity = modelBuilder.Entity<Customer>();
            var adminEntity = modelBuilder.Entity<Admin>();
            var menuEntity = modelBuilder.Entity<Menu>();

            
            //Lite seedat data
            // Table
            tableEntity.HasData(
                new ResturantTable { Id = 1, Number = 1, Capacity = 2 },
                new ResturantTable { Id = 2, Number = 2, Capacity = 4},
                new ResturantTable { Id = 3, Number = 3, Capacity = 6},
                new ResturantTable { Id = 4, Number = 4, Capacity = 8 }
                );

            // Customer
            customerEntity.HasData(
                new Customer { Id = 1, Name = "Lasse Ricardo", Phone = "0703873563", Email = "test@gmail.com"},
                new Customer { Id = 2, Name = "Alfie Smith", Phone = "0765376534", Email = "nytest@gmail.com"},
                new Customer { Id = 3, Name = "Karin Andersson", Phone = "0734445566", Email = "karin@gmail.com" },
                new Customer { Id = 4, Name = "Ollie Paul", Phone = "0708889999", Email = "olliee@live.se" }
                );

            // Admin
            adminEntity.HasData(
                new Admin { Id = 1, Username = "admin", PasswordHash = "testhash123" }
                );

            // Menu
            menuEntity.HasData(
                new Menu { Id = 1, Name = "Pizza Margherita", Description = "Klassisk pizza med mozzarella", Price = 99, IsPopular = true },
                new Menu { Id = 2, Name = "Caesarsallad", Description = "Sallad med kyckling och parmesan", Price = 120, IsPopular = false },
                new Menu { Id = 3, Name = "Lasagne al Forno", Description = "Husets lasagne med köttfärs, ost och tomatsås.", Price = 135, IsPopular = true },
                new Menu { Id = 4, Name = "Tiramisu", Description = "Italiensk dessert med mascarpone och espresso.", Price = 75, IsPopular = true },
                new Menu { Id = 5, Name = "Vegetarisk pasta", Description = "Pasta med grillade grönsaker och pesto.", Price = 110, IsPopular = false }
                );
        }

    }
}
