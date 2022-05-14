using Parser.Core.Application.Models;

namespace Parser.Core.Application.Commands.ReplaceOneParserDynamicEntityModel
{
    public class ReplaceOneParserDynamicEntityModelCommand : IRequest<ParserDynamicEntityModel>
    {
        public ParserDynamicEntityModel Model { get; set; }
    }
}
