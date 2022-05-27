namespace Parser.Core.Application.Commands.ReplaceOneParserDynamicEntityModel
{
    public class ReplaceOneParserDynamicEntityModelCommandValidator
        : AbstractValidator<ReplaceOneParserDynamicEntityModelCommand>
    {
        public ReplaceOneParserDynamicEntityModelCommandValidator()
        {
            var objectIdLengthConstraint = 24;

            RuleFor(command => command).NotEmpty();
            RuleFor(command => command.Id)
                .NotEmpty()
                .Length(objectIdLengthConstraint);
            RuleFor(command => command.EntityName).NotEmpty();
            RuleFor(command => command.Properties).NotEmpty();

            RuleForEach(command => command.Properties)
                .SetValidator(new DynamicEntityModelPropertyValidator());
        }
    }
}
