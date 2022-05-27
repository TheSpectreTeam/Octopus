namespace Loader.Core.Application.Features.DynamicEntity.Queries.GetDynamicEntityById
{
    public class GetDynamicEntityQueryValidator : AbstractValidator<GetDynamicEntityQuery>
    {
        public GetDynamicEntityQueryValidator()
        {
            RuleFor(_ => _.Id)
                .NotEmpty().WithMessage(ValidationMessages.ValueIsRequired)
                .NotNull()
                .Length(24).WithMessage(ValidationMessages.InvalidIdLength);
        }
    }
}
