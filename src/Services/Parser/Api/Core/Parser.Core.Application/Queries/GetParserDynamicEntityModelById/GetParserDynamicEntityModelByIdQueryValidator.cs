namespace Parser.Core.Application.Queries.GetParserDynamicEntityModelById
{
    public class GetParserDynamicEntityModelByIdQueryValidator 
        : AbstractValidator<GetParserDynamicEntityModelByIdQuery>
    {
        public GetParserDynamicEntityModelByIdQueryValidator()
        {
            var objectIdLengthConstraint = 24;

            RuleFor(query => query.Id)
                .NotEmpty()
                .Length(objectIdLengthConstraint);
        }
    }
}
