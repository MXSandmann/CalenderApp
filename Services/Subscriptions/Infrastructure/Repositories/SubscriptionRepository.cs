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
            return await _context.Subscriptions.ToListAsync();
        }

        public Task<Subscription> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Remove(Subscription subscription)
        {
            throw new NotImplementedException();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Task<Subscription> Update(Subscription subscription)
        {
            throw new NotImplementedException();
        }
    }
}
