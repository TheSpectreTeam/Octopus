using Parser.Core.Application.Models;

namespace Parser.Core.Application.Commands.CreateManyParserDynamicEntityModels
{
    public class CreateManyParserDynamicEntityModelsCommand : IRequest
    {
        public IEnumerable<ParserDynamicEntityModel> Models { get; set; }
    }
}
