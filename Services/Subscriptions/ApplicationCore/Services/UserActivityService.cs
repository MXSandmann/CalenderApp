using ApplicationCore.Models.Documents;
using ApplicationCore.Repositories.Contracts;
using ApplicationCore.Services.Contracts;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ApplicationCore.Services
{
    public class UserActivityService : IUserActivityService
    {
        private readonly IUserActivityRepository _repository;
        private readonly ILogger<IUserActivityService> _logger;

        public UserActivityService(IUserActivityRepository repository, ILogger<IUserActivityService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task Create(UserActivityRecord record)
        {
            _logger.LogDebug("--> Creating a record: {value}", JsonConvert.SerializeObject(record));
            await _repository.Create(record);
        }

        public async Task<IEnumerable<UserActivityRecord>> GetAll()
        {
            var records = await _repository.GetAll();
            _logger.LogDebug("--> Got records: {value}", JsonConvert.SerializeObject(records));
            return records;
        }

        public async Task<UserActivityRecord> GetById(string id)
        {
            var record = await _repository.GetById(id);
            _logger.LogDebug("--> Got a record: {value}", JsonConvert.SerializeObject(record));
            return record;
        }
    }
}
