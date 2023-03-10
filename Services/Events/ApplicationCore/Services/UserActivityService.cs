using ApplicationCore.Models.Documents;
using ApplicationCore.Repositories.Contracts;
using ApplicationCore.Services.Contracts;

namespace ApplicationCore.Services
{
    public class UserActivityService : IUserActivityService
    {
        private readonly IUserActivityRepository _repository;

        public UserActivityService(IUserActivityRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserActivityRecord> Create(UserActivityRecord record)
        {
            return await _repository.Create(record);
        }

        public async Task<IEnumerable<UserActivityRecord>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<UserActivityRecord> GetById(string id)
        {
            return await _repository.GetById(id);
        }
    }
}
