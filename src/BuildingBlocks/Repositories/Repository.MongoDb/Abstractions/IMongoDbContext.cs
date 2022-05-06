namespace Repository.MongoDb.Abstractions
{
    public interface IMongoDbContext<T> where T : IMongoEntityBase
    {
        IMongoCollection<T> GetMongoCollection();
    }
}
