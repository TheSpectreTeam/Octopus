namespace Parser.Core.Application.Commands.DeleteOneParserDynamicEntityModel
{
    public class DeleteOneParserDynamicEntityModelCommand : IRequest
    {
        public Expression<Func<ParserDynamicEntityModel, bool>> FilterExpression;
    }
}
