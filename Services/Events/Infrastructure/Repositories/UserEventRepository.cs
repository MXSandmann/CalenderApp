using ApplicationCore.Models.Entities;
using ApplicationCore.Repositories.Contracts;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

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
            return await _context.UserEvents.Include(x => x.RecurrencyRule).ToListAsync();
        }

        public async Task<UserEvent> GetById(Guid id)
        {
            var userEvent = await _context.UserEvents.Include(x => x.RecurrencyRule).FirstOrDefaultAsync(x => x.Id == id);
            ArgumentNullException.ThrowIfNull(userEvent);
            return userEvent;
        }

        public async Task<Guid> Add(UserEvent userEvent)
        {
            await _context.UserEvents.AddAsync(userEvent);
            await _context.SaveChangesAsync();
            return userEvent.Id;
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

        public async Task<UserEvent> Update(UserEvent userEvent)
        {
            _context.UserEvents.Update(userEvent);
            await _context.SaveChangesAsync();
            var updated = await _context.UserEvents.Include(x => x.RecurrencyRule).FirstOrDefaultAsync(x => x.Id == userEvent.Id);
            ArgumentNullException.ThrowIfNull(updated);
            return updated;
        }

        public async Task AddRange(IEnumerable<UserEvent> userEvents)
        {
            await _context.AddRangeAsync(userEvents);
            await _context.SaveChangesAsync();
        }

        public async Task<Dictionary<Guid, string>> GetEventNames(IEnumerable<Guid> eventIds)
        {
            return await _context.UserEvents.Where(ue => eventIds.Contains(ue.Id)).ToDictionaryAsync(ue => ue.Id, ue => ue.Name);
        }

        public async Task<(IEnumerable<UserEvent>, int)> SearchUserEvents(string entry, int limit, int offset)
        {
            var query = _context.UserEvents.Where(x => x.Name.Contains(entry)
            || x.Place.Contains(entry)
            || x.Description.Contains(entry));

            var count = await query.CountAsync();

            var results = await query.Skip(offset)
                .Take(limit)
                .ToListAsync();

            return (results, count);
        }
    }
}
