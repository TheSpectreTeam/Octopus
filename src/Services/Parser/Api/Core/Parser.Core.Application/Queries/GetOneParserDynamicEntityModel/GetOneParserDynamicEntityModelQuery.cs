namespace Parser.Core.Application.Queries.GetOneParserDynamicEntityModel
{
    public class GetOneParserDynamicEntityModelQuery : IRequest<ParserDynamicEntityModel>
    {
        public Expression<Func<ParserDynamicEntityModel, bool>> FilterExpression { get; set; }
    }
}
