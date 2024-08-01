using Microsoft.EntityFrameworkCore;

namespace Pakiza.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Optionally, add some seed data here
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, CustomerName = "Mr. Rahat Ahmed", Phone = "123456789" },
                new Customer { Id = 2, CustomerName = "Mr. Aowal Azad", Phone = "123456789" }
            );
        }
    }
}
