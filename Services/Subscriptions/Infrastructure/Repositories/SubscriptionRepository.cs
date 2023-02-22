using ApplicationCore.Models.Entities;
using ApplicationCore.Repositories;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly SubscriptionDataContext _context;

        public SubscriptionRepository(SubscriptionDataContext context)
        {
            _context = context;
        }

        public async Task<Subscription> Add(Subscription subscription)
        {
            await _context.Subscriptions.AddAsync(subscription);
            await _context.SaveChangesAsync();
            return subscription;
        }

        public async Task<IEnumerable<Subscription>> GetAll()
        {
            return await _context.Subscriptions.Include(x => x.Notifications).ToListAsync();
        }

        public async Task<Subscription> GetById(Guid id)
        {
            var subscription = await _context.Subscriptions.FirstOrDefaultAsync(x => x.Id == id);
            ArgumentNullException.ThrowIfNull(subscription);
            return subscription;
        }

        public async Task Remove(Subscription subscription)
        {
            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Subscription> Update(Subscription subscription)
        {
            var toUpdate = await _context.Subscriptions.FindAsync(subscription.Id);
            ArgumentNullException.ThrowIfNull(toUpdate);

            toUpdate.UserName = subscription.UserName;
            toUpdate.UserEmail = subscription.UserEmail;

            await _context.SaveChangesAsync();
            return toUpdate;
        }
    }
}
