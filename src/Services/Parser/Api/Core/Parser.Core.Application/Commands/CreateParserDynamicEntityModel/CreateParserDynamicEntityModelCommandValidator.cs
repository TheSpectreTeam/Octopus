namespace Parser.Core.Application.Commands.CreateParserDynamicEntityModel
{
    public class CreateParserDynamicEntityModelCommandValidator
        : AbstractValidator<CreateParserDynamicEntityModelCommand>
    {
        public CreateParserDynamicEntityModelCommandValidator()
        {
            RuleFor(command => command).NotEmpty();
            RuleFor(command => command.EntityName).NotEmpty();
            RuleFor(command => command.Properties).NotEmpty();
        }
    }
}
