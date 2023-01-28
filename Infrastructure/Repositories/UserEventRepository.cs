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

        public async Task<IEnumerable<UserEvent>> GetAll(string sortBy)
        {            
            if (sortBy != null
                && sortBy.Equals("Place"))
                return await _context.UserEvents.OrderBy(x => x.Place).ToListAsync();
            if (sortBy != null
                && sortBy.Equals("Category"))
                return await _context.UserEvents.OrderBy(x => x.Category).ToListAsync();
            return await _context.UserEvents.OrderBy(x => x.Date).ThenBy(x => x.StartTime).ToListAsync();
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
