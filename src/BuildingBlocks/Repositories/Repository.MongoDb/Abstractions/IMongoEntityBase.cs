namespace Repository.MongoDb.Abstractions
{
    public interface IMongoEntityBase : IBaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }
        DateTime CreateAt { get; }
    }
}
