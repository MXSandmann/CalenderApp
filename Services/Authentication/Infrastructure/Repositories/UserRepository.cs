using ApplicationCore.Models.Entities;
using ApplicationCore.Models.Enums;
using ApplicationCore.Repositories.Contracts;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDataContext _context;

        public UserRepository(UserDataContext context)
        {
            _context = context;
        }

        public async Task<User> AddNewUser(User user)
        {
            var newUser = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return newUser.Entity;

        }

        public async Task<User?> GetUser(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.UserName.Equals(username));
        }

        public async Task<IEnumerable<User>> GetAllInstructors()
        {
            return await _context.Users.Where(x => x.Role == Role.Instructor).ToListAsync();            
        }
    }
}
