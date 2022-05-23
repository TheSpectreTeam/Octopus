namespace Parser.Core.Application.Commands.CreateParserDynamicEntityModel
{
    public class CreateParserDynamicEntityModelCommand : IRequest<Response<string>>
    {
        public string EntityName { get; set; }
        public IEnumerable<DynamicEntityModelProperty> Properties { get; set; }
    }
}
