using Microsoft.EntityFrameworkCore;
using TastyByThreeMusketeers.Core.Models;

namespace TastyByThreeMusketeers.Infrastructure.Data
{
    public class CanteenDbContext : DbContext
    {
        public CanteenDbContext(DbContextOptions<CanteenDbContext> options)
            : base(options)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
    }
}
