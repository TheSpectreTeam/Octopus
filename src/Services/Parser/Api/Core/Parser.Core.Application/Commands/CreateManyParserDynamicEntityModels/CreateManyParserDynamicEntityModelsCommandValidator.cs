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
                .ChildRules(model =>
                {
                    model.RuleFor(_ => _.EntityName).NotEmpty();
                    model.RuleFor(_ => _.Properties).NotEmpty();
                });;
        }
    }
}
