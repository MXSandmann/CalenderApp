using ApplicationCore.Models.Documents;

namespace ApplicationCore.Services.Contracts
{
    public interface IUserActivityService
    {
        Task<IEnumerable<UserActivityRecord>> GetAll();
        Task<UserActivityRecord> Create(UserActivityRecord record);
        Task<UserActivityRecord> GetById(string id);
    }
}
