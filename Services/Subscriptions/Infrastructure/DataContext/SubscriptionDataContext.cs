using ApplicationCore.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataContext
{
    public class SubscriptionDataContext : DbContext
    {
        public SubscriptionDataContext(DbContextOptions<SubscriptionDataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SubscriptionDataContext).Assembly);
        }

        public DbSet<Subscription> Subscriptions { get; set; } = null!;
        public DbSet<Notification> Notifications { get; set; } = null!;
    }
}
