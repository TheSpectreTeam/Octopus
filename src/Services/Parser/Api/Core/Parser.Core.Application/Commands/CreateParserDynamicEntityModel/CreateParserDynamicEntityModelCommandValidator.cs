namespace Parser.Core.Application.Commands.CreateParserDynamicEntityModel
{
    public class CreateParserDynamicEntityModelCommandValidator
        : AbstractValidator<CreateParserDynamicEntityModelCommand>
    {
        public CreateParserDynamicEntityModelCommandValidator()
        {
            var objectIdLengthConstraint = 24;

            RuleFor(command => command).NotEmpty();
            RuleFor(command => command.Model.Id)
                .NotEmpty()
                .Length(objectIdLengthConstraint);
            RuleFor(command => command.EntityName).NotEmpty();
            RuleFor(command => command.Properties).NotEmpty();
        }
    }
}
