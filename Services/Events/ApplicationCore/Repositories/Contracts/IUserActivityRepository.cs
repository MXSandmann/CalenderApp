using ApplicationCore.Models.Documents;

namespace ApplicationCore.Repositories.Contracts
{
    public interface IUserActivityRepository
    {
        Task<IEnumerable<UserActivityRecord>> GetAll();
        Task<UserActivityRecord> GetById(string id);
        Task Create(UserActivityRecord record);
    }
}
