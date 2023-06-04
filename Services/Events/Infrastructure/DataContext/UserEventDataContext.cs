using ApplicationCore.Models.Entities;
using ApplicationCore.Models.Enums;
using ApplicationCore.Models.Notifications;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataContext
{
    public class UserEventDataContext : DbContext
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserEventDataContext(DbContextOptions<UserEventDataContext> options, IMediator mediator, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserEventDataContext).Assembly);
        }

        public DbSet<UserEvent> UserEvents { get; set; } = null!;
        public DbSet<RecurrencyRule> RecurrencyRules { get; set; } = null!;

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var user = _httpContextAccessor.HttpContext.User.Identities.First().Claims.FirstOrDefault(x => x.Type.Equals("UserName"))?.Value ?? "User";            
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
