using Microsoft.EntityFrameworkCore;
using ZeroTask.DAL.Entities;
using ZeroTask.BLL.Repositories.Contracts;
using ZeroTask.DAL.DataContext;

namespace ZeroTask.BLL.Repositories
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

        public async Task<UserEvent?> GetById(Guid id)
        {
            return await _context.UserEvents.FindAsync(id);
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
    }
}
