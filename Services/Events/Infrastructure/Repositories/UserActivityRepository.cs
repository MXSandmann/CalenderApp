using ApplicationCore.Models.Documents;
using ApplicationCore.Options;
using ApplicationCore.Repositories.Contracts;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class UserActivityRepository : IUserActivityRepository
    {
        private readonly IMongoCollection<UserActivityRecord> _collection;

        public UserActivityRepository(IOptions<NoSqlSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _collection = mongoDb.GetCollection<UserActivityRecord>(settings.Value.CollectionName);
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
