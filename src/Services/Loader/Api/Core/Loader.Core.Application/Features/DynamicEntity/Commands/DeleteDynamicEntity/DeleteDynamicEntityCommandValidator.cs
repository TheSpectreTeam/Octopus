namespace Loader.Core.Application.Features.DynamicEntity.Commands.DeleteDynamicEntity
{
    public class DeleteDynamicEntityCommandValidator : AbstractValidator<DeleteDynamicEntityCommand>
    {
        public DeleteDynamicEntityCommandValidator()
        {
            RuleFor(_ => _.Id)
                .NotNull()
                .NotEmpty()
                .Length(24);
        }
    }
}
