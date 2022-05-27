namespace Parser.Core.Application.Commands.CreateManyParserDynamicEntityModels
{
    public class CreateManyParserDynamicEntityModelsCommandValidator
        : AbstractValidator<CreateManyParserDynamicEntityModelsCommand>
    {
        public CreateManyParserDynamicEntityModelsCommandValidator()
        {
            RuleFor(command => command).NotEmpty();
            RuleFor(command => command.Models).NotNull();

            RuleForEach(command => command.Models)
                .SetValidator(new CreateParserDynamicEntityModelCommandValidator());
        }
    }
}
