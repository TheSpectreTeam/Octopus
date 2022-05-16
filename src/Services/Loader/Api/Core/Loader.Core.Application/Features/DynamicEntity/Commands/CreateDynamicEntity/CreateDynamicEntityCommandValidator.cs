namespace Loader.Core.Application.Features.DynamicEntity.Commands.CreateDynamicEntity
{
    public class CreateDynamicEntityCommandValidator 
        : AbstractValidator<CreateDynamicEntityCommand>
    {
        public CreateDynamicEntityCommandValidator()
        {
            RuleFor(_ => _.EntityName)
                .NotNull()
                .NotEmpty();
            RuleFor(_ => _.Properties)
                .NotEmpty();

            RuleFor(_ => _.Properties
                .Select(i => i.PropertyName))
                    .NotNull()
                    .NotEmpty();
            RuleFor(_ => _.Properties
                .Select(i => i.SystemTypeName))
                    .NotNull()
                    .NotEmpty();
        }
    }
}
