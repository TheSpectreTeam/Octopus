namespace Parser.Core.Application.Commands.CreateParserDynamicEntityModel
{
    public class CreateParserDynamicEntityModelCommandValidator
        : AbstractValidator<CreateParserDynamicEntityModelCommand>
    {
        public CreateParserDynamicEntityModelCommandValidator()
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
