using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApplicationCore.Models.Documents
{
    public class UserActivityRecord
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string UserAction { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Synopsis { get; set; } = string.Empty;
        public DateTime TimeOfAction { get; set; }
    }
}
