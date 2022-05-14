namespace Parser.Core.Application.Commands.ReplaceOneParserDynamicEntityModel
{
    public class ReplaceOneParserDynamicEntityModelCommandValidator
        : AbstractValidator<ReplaceOneParserDynamicEntityModelCommand>
    {
        public ReplaceOneParserDynamicEntityModelCommandValidator()
        {
            var objectIdLengthConstraint = 24;

            RuleFor(command => command.Model).NotEmpty();
            RuleFor(command => command.Model.Id)
                .NotEmpty()
                .Length(objectIdLengthConstraint);
            RuleFor(command => command.Model.EntityName).NotEmpty();
            RuleFor(command => command.Model.Properties).NotEmpty();
        }
    }
}
