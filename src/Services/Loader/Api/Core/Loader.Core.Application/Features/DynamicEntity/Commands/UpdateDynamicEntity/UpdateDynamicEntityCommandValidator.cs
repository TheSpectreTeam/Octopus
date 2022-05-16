namespace Loader.Core.Application.Features.DynamicEntity.Commands.UpdateDynamicEntity
{
    public class UpdateDynamicEntityCommandValidator 
        : AbstractValidator<UpdateDynamicEntityCommand>
    {
        public UpdateDynamicEntityCommandValidator()
        {
            RuleFor(_ => _.Id)
                .NotNull()
                .NotEmpty()
                .Length(24);
            RuleFor(_ => _.EntityName)
                .NotNull()
                .NotEmpty();
            RuleFor(_ => _.Properties)
                .NotNull()
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
