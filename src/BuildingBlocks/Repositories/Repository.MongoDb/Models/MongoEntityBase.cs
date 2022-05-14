namespace Repository.MongoDb.Models
{
    public class MongoEntityBase : IMongoEntityBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
