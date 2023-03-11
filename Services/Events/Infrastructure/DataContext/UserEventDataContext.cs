using ApplicationCore.Models.Entities;
using ApplicationCore.Models.Enums;
using ApplicationCore.Models.Notifications;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataContext
{
    public class UserEventDataContext : DbContext
    {
        private readonly IMediator _mediator;        
        public UserEventDataContext(DbContextOptions<UserEventDataContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserEventDataContext).Assembly);
        }

        public DbSet<UserEvent> UserEvents { get; set; } = null!;
        public DbSet<RecurrencyRule> RecurrencyRules { get; set; } = null!;

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var user = "User";            
            var entries = ChangeTracker.Entries<UserEvent>();
            foreach (var entry in entries)
            {
                if (entry.State is EntityState.Added)
                    await _mediator.Publish(new OnUserActionNotification(UserActionOnEvent.Created, user, $"{user} created an event"), cancellationToken);
                if (entry.State is EntityState.Modified)
                    await _mediator.Publish(new OnUserActionNotification(UserActionOnEvent.Updated, user, $"{user} updated an event"), cancellationToken);
                if (entry.State is EntityState.Deleted)
                    await _mediator.Publish(new OnUserActionNotification(UserActionOnEvent.Deleted, user, $"{user} deleted an event"), cancellationToken);
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
