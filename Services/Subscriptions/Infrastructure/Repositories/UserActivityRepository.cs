using ApplicationCore.ConfigConstants;
using ApplicationCore.Models.Documents;
using ApplicationCore.Repositories.Contracts;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class UserActivityRepository : IUserActivityRepository
    {
        private readonly IMongoCollection<UserActivityRecord> _collection;

        public UserActivityRepository(IConfiguration configuration)
        {
            var mongoClient = new MongoClient(configuration.GetValue<string>(Config.MongoDB.ConnectionString));
            var mongoDb = mongoClient.GetDatabase(configuration.GetValue<string>(Config.MongoDB.DatabaseName));
            _collection = mongoDb.GetCollection<UserActivityRecord>(configuration.GetValue<string>(Config.MongoDB.CollectionName));
        }

        public async Task Create(UserActivityRecord record)
        {
            await _collection.InsertOneAsync(record);
        }

        public async Task<IEnumerable<UserActivityRecord>> GetAll()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<UserActivityRecord> GetById(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
