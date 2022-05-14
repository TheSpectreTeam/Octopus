using Parser.Core.Application.Models;

namespace Parser.Core.Application.Commands.CreateParserDynamicEntityModel
{
    public class CreateParserDynamicEntityModelCommand : IRequest
    {
        public ParserDynamicEntityModel Model { get; set; }
    }
}
