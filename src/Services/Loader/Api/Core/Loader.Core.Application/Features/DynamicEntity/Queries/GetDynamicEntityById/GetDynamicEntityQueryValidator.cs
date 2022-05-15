namespace Loader.Core.Application.Features.DynamicEntity.Queries.GetDynamicEntityById
{
    public class GetDynamicEntityQueryValidator : AbstractValidator<GetDynamicEntityQuery>
    {
        public GetDynamicEntityQueryValidator()
        {
            RuleFor(_ => _.Id)
                .NotNull()
                .NotEmpty()
                .Length(24);
        }
    }
}
