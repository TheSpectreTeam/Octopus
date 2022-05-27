namespace Parser.Core.Application.Commands.ReplaceOneParserDynamicEntityModel
{
    public class ReplaceOneParserDynamicEntityModelCommand : IRequest<Response<ParserDynamicEntityModel>>
    {
        public string Id { get; set; }
        public string EntityName { get; set; }
        public IEnumerable<DynamicEntityModelProperty> Properties { get; set; }
    }
}
