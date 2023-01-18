using Microsoft.EntityFrameworkCore;
using ApplicationCore.Models;
using ApplicationCore.Repositories.Contracts;
using Infrastructure.DataContext;

namespace Infrastructure.Repositories
{
    public class UserEventRepository : IUserEventRepository
    {
        private readonly UserEventDataContext _context;
        public UserEventRepository(UserEventDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserEvent>> GetAll()
        {
            return await _context.UserEvents.ToListAsync();
        }

        public async Task<UserEvent> GetById(Guid id)
        {
            var userEvent = await _context.UserEvents.FindAsync(id);
            ArgumentNullException.ThrowIfNull(userEvent);
            return userEvent;
        }

        public async Task Add(UserEvent userEvent)
        {
            await _context.UserEvents.AddAsync(userEvent);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(UserEvent userEvent)
        {
            _context.UserEvents.Remove(userEvent);
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(UserEvent userEvent)
        {
            _context.UserEvents.Update(userEvent);
            await _context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<UserEvent> userEvents)
        {
            await _context.AddRangeAsync(userEvents);
            await _context.SaveChangesAsync();
        }
    }
}
