using DataAccess.Configurations;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class DataContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Provider> Providers { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderConfiguration).Assembly);

            modelBuilder.Entity<Provider>().HasData(
                new { Id = 1, Name = "IKEA" },
                new { Id = 2, Name = "Amazon" },
                new { Id = 3, Name = "Ebay" },
                new { Id = 4, Name = "AliExpress" }
            );
        }
    }
}