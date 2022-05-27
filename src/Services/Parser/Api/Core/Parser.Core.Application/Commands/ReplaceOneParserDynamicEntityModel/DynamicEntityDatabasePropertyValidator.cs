namespace Parser.Core.Application.Commands.ReplaceOneParserDynamicEntityModel
{
    public class DynamicEntityDatabasePropertyValidator
        : AbstractValidator<DynamicEntityDatabaseProperty>
    {
        public DynamicEntityDatabasePropertyValidator()
        {
            RuleFor(_ => _.DatabaseTypeName)
                .NotEmpty()
                .MinimumLength(2);
        }
    }
}
