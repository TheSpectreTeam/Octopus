namespace Parser.Core.Application.Queries.GetParserDynamicEntityModelById
{
    public class GetParserDynamicEntityModelByIdQuery : IRequest<Response<ParserDynamicEntityModel>>
    {
        public string Id { get; set; }
    }
}
