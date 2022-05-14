namespace Repository.MongoDb.Abstractions
{
    public interface IMongoEntityBase : IBaseEntity
    {
        string Id { get; set; }
    }
}
