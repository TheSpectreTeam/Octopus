using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Repository.MongoDb.Abstractions;
using Common.Models.DynamicEntity;

namespace Loader.Core.Domain.Models
{
    public class LoaderDynamicEntity : DynamicEntityModel, IMongoEntityBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
