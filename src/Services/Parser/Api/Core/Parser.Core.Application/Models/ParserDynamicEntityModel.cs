namespace Parser.Core.Application.Models
{
    public class ParserDynamicEntityModel : DynamicEntityModel, IMongoEntityBase
    {
        public string Id { get; set; }
    }
}
