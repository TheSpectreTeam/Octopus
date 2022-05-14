namespace Loader.Core.Application.Features.DynamicEntity.Commands.DeleteDynamicEntity
{
    public class DeleteDynamicEntityValidator : AbstractValidator<DeleteDynamicEntityCommand>
    {
        public DeleteDynamicEntityValidator()
        {
            RuleFor(_ => _.Id)
                .NotNull()
                .NotEmpty()
                .Length(24);
        }
    }
}
