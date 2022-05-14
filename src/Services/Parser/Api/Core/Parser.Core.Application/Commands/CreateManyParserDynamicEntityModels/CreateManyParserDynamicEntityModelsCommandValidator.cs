namespace Parser.Core.Application.Commands.CreateManyParserDynamicEntityModels
{
    public class CreateManyParserDynamicEntityModelsCommandValidator
        : AbstractValidator<CreateManyParserDynamicEntityModelsCommand>
    {
        public CreateManyParserDynamicEntityModelsCommandValidator()
        {
            RuleFor(command => command.Models).NotNull();

            RuleForEach(command => command.Models)
                .ChildRules(model =>
                {
                    var objectIdLengthConstraint = 24;
                    model.RuleFor(_ => _.Id)
                        .NotEmpty()
                        .Length(objectIdLengthConstraint);

                    model.RuleFor(_ => _.EntityName).NotEmpty();
                    model.RuleFor(_ => _.Properties).NotEmpty();
                });;
        }
    }
}
