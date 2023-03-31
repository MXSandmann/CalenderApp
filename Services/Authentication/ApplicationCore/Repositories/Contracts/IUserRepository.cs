using ApplicationCore.Models.Entities;

namespace ApplicationCore.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task<User?> GetUser(string username);
        Task<User> AddNewUser(User user);
        Task<IEnumerable<User>> GetAllInstructors();
    }
}
