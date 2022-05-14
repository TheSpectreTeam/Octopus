using Parser.Core.Application.Models;

namespace Parser.Core.Application.Queries.GetParserDynamicEntityModelById
{
    public class GetParserDynamicEntityModelByIdQuery : IRequest<ParserDynamicEntityModel>
    {
        public object Id { get; set; }
    }
}
