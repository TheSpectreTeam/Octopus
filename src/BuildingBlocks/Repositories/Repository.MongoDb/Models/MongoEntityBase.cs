namespace Repository.MongoDb.Models
{
    public class MongoEntityBase : IMongoEntityBase
    {
        public ObjectId Id { get; set; }
        public DateTime CreateAt => Id.CreationTime;
    }
}
