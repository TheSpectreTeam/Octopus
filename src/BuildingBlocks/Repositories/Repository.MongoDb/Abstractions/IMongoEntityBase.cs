namespace Repository.MongoDb.Abstractions
{
    public interface IMongoEntityBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }
        DateTime CreateAt { get; }
    }
}
