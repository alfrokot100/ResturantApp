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

            // Table
            tableEntity.HasData(
                new ResturantTable { Id = 1, Number = 1, Capacity = 2 },
                new ResturantTable { Id = 2, Number = 2, Capacity = 4},
                new ResturantTable { Id = 3, Number = 3, Capacity = 6}
                );

            // Customer
            customerEntity.HasData(
                new Customer { Id = 1, Name = "Lasse Ricardo", Phone = "0703873563", Email = "test@gmail.com"},
                new Customer { Id = 2, Name = "Alfie Smith", Phone = "0765376534", Email = "nytest@gmail.com"}
                );

            // Admin
            adminEntity.HasData(
                new Admin { Id = 1, Username = "admin", PasswordHash = "testhash123" }
                );

            // Menu
            menuEntity.HasData(
                new Menu { Id = 1, Name = "Pizza Margherita", Description = "Klassisk pizza med mozzarella", Price = 99, IsPopular = true },
                new Menu { Id = 2, Name = "Caesarsallad", Description = "Sallad med kyckling och parmesan", Price = 120, IsPopular = false }
                );
        }

    }
}
