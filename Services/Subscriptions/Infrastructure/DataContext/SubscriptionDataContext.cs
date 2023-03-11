using ApplicationCore.Models.Entities;
using ApplicationCore.Models.Enums;
using ApplicationCore.Models.Notifications;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataContext
{
    public class SubscriptionDataContext : DbContext
    {
        private readonly IMediator _mediator;
        public SubscriptionDataContext(DbContextOptions<SubscriptionDataContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SubscriptionDataContext).Assembly);
        }

        public DbSet<Subscription> Subscriptions { get; set; } = null!;
        public DbSet<Notification> Notifications { get; set; } = null!;

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<Subscription>();
            foreach (var entry in entries)
            {
                var userName = entry.Entity.UserName;
                var eventId = entry.Entity.EventId;
                if (entry.State is EntityState.Added)
                    await _mediator.Publish(new OnUserActionNotification(UserActionOnEvent.Subscribed, userName, $"{userName} subscribed to an event with id: {eventId}"), cancellationToken);
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
