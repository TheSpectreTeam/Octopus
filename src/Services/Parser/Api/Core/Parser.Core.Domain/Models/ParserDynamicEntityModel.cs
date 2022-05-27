using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Repository.MongoDb.Abstractions;
using Common.Models.DynamicEntity;

namespace Parser.Core.Domain.Models
{
    public class ParserDynamicEntityModel : DynamicEntityModel, IMongoEntityBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
