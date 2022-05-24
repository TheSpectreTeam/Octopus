using Parser.Core.Application.Commands.CreateParserDynamicEntityModel;

namespace Parser.Core.Application.Commands.CreateManyParserDynamicEntityModels
{
    public class CreateManyParserDynamicEntityModelsCommand : IRequest<Response<IDictionary<int, string>>>
    {
        public IEnumerable<CreateParserDynamicEntityModelCommand> Models { get; set; }
    }
}
