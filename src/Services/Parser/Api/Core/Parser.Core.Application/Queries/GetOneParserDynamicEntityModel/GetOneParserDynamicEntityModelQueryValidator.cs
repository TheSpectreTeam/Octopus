namespace Parser.Core.Application.Queries.GetOneParserDynamicEntityModel
{
    public class GetOneParserDynamicEntityModelQueryValidator
        : AbstractValidator<GetOneParserDynamicEntityModelQuery>
    {
        public GetOneParserDynamicEntityModelQueryValidator()
        {
            RuleFor(query => query.FilterExpression).NotEmpty();
        }
    }
}
