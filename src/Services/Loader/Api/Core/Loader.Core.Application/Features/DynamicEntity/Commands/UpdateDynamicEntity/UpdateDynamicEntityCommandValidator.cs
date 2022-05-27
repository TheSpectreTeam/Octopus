namespace Loader.Core.Application.Features.DynamicEntity.Commands.UpdateDynamicEntity
{
    public class UpdateDynamicEntityCommandValidator 
        : AbstractValidator<UpdateDynamicEntityCommand>
    {
        public UpdateDynamicEntityCommandValidator()
        {
            RuleFor(_ => _.Id)
                .NotEmpty().WithMessage(ValidationMessages.ValueIsRequired)
                .NotNull()
                .Length(24).WithMessage(ValidationMessages.InvalidIdLength);

            RuleFor(_ => _.EntityName)
                .NotEmpty().WithMessage(ValidationMessages.ValueIsRequired)
                .NotNull();

            RuleFor(_ => _.Properties)
                .NotEmpty().WithMessage(ValidationMessages.ValueIsRequired)
                .NotNull();

            RuleForEach(_ => _.Properties)
                .SetValidator(new DynamicEntityModelPropertyValidator());
        }
        public class DynamicEntityModelPropertyValidator
            : AbstractValidator<DynamicEntityModelProperty>
        {
            public DynamicEntityModelPropertyValidator()
            {
                RuleFor(_ => _.PropertyName)
                    .NotEmpty().WithMessage(ValidationMessages.ValueIsRequired)
                    .NotNull();
                RuleFor(_ => _.SystemTypeName)
                    .NotEmpty().WithMessage(ValidationMessages.ValueIsRequired)
                    .NotNull();
                RuleFor(_ => _.DatabaseEntityProperty)
                    .NotEmpty().WithMessage(ValidationMessages.ValueIsRequired)
                    .NotNull()
                    .SetValidator(new DynamicEntityDatabasePropertyValidator());
            }

            public class DynamicEntityDatabasePropertyValidator
                : AbstractValidator<DynamicEntityDatabaseProperty>
            {
                public DynamicEntityDatabasePropertyValidator()
                {
                    RuleFor(_ => _.DatabaseTypeName)
                        .NotEmpty().WithMessage(ValidationMessages.ValueIsRequired)
                        .NotNull()
                        .MinimumLength(2).WithMessage(ValidationMessages.DbNameInvalidLength);
                }
            }
        }
    }
}
