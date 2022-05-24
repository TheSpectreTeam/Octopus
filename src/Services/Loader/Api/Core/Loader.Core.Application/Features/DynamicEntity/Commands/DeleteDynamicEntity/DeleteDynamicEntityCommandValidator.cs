namespace Loader.Core.Application.Features.DynamicEntity.Commands.DeleteDynamicEntity
{
    public class DeleteDynamicEntityCommandValidator : AbstractValidator<DeleteDynamicEntityCommand>
    {
        public DeleteDynamicEntityCommandValidator()
        {
            RuleFor(_ => _.Id)
                .NotEmpty().WithMessage(ValidationMessages.ValueIsRequired)
                .NotNull()
                .Length(24).WithMessage(ValidationMessages.InvalidIdLength);
        }
    }
}
