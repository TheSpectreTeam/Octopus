using Parser.Core.Application.Models;

namespace Parser.Core.Application.Commands.DeleteManyParserDynamicEntityModel
{
    public class DeleteManyParserDynamicEntityModelCommand : IRequest
    {
        public Expression<Func<ParserDynamicEntityModel, bool>> FilterExpression;
    }
}
