namespace Parser.Core.Application.Commands.ReplaceOneParserDynamicEntityModel
{
    public class DynamicEntityModelPropertyValidator
        : AbstractValidator<DynamicEntityModelProperty>
    {
        public DynamicEntityModelPropertyValidator()
        {
            RuleFor(_ => _.PropertyName).NotEmpty();

            RuleFor(_ => _.SystemTypeName).NotEmpty();

            RuleFor(_ => _.ValueIndex).GreaterThanOrEqualTo(0);

            RuleFor(_ => _.DatabaseEntityProperty)
                .NotEmpty()
                .SetValidator(new DynamicEntityDatabasePropertyValidator());
        }
    }
}
