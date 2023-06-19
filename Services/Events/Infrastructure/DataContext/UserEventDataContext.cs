using ApplicationCore.Models.Entities;
using ApplicationCore.Models.Enums;
using ApplicationCore.Models.Notifications;
using ApplicationCore.Providers.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataContext
{
    public class UserEventDataContext : DbContext
    {
        private readonly IMediator _mediator;
        private readonly IUserNameProvider _userNameProvider;
        public UserEventDataContext(DbContextOptions<UserEventDataContext> options, IMediator mediator, IUserNameProvider userNameProvider) : base(options)
        {
            _mediator = mediator;
            _userNameProvider = userNameProvider;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserEventDataContext).Assembly);
        }

        public DbSet<UserEvent> UserEvents { get; set; } = null!;
        public DbSet<RecurrencyRule> RecurrencyRules { get; set; } = null!;

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {            
            var user = _userNameProvider.GetUserName();
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
