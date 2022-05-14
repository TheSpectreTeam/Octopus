namespace Parser.Core.Application.Queries.GetParserDynamicEntityModelById
{
    public class GetParserDynamicEntityModelByIdQueryValidator 
        : AbstractValidator<GetParserDynamicEntityModelByIdQuery>
    {
        public GetParserDynamicEntityModelByIdQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
