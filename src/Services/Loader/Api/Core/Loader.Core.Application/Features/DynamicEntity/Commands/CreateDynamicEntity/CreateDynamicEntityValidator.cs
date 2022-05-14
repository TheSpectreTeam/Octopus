namespace Loader.Core.Application.Features.DynamicEntity.Commands.CreateDynamicEntity
{
    public class CreateDynamicEntityValidator : AbstractValidator<CreateDynamicEntityCommand>
    {
        public CreateDynamicEntityValidator()
        {
            RuleFor(_ => _.EntityName)
                .NotNull()
                .NotEmpty();
            RuleFor(_ => _.Properties)
                .NotEmpty();
        }
    }
}
