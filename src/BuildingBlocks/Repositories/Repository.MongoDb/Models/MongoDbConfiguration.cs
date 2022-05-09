namespace Repository.MongoDb.Models
{
    public class MongoDbConfiguration : IMongoDbConfiguration
    {
        public string DatabaseName { get; set; } = "local";
        public string ServerIp { get; set; } = "localhost";
        public int? Port { get; set; } = 27017;

        public string ConnectionString
            => $"mongodb://{ServerIp}:{Port}/{DatabaseName}";
    }
}