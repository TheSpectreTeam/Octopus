namespace Repository.MongoDb.Abstractions
{
    public interface IMongoDbConfiguration
    {
        string DatabaseName { get; set; }
        string ServerIp { get; set; }
        int? Port { get; set; }
        string ConnectionString { get; }
    }
}
