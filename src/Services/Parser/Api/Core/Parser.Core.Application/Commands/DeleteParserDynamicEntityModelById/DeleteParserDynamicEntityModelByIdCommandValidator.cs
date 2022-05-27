namespace Parser.Core.Application.Commands.DeleteParserDynamicEntityModelById
{
    public class DeleteParserDynamicEntityModelByIdCommandValidator
        : AbstractValidator<DeleteParserDynamicEntityModelByIdCommand>
    {
        public DeleteParserDynamicEntityModelByIdCommandValidator()
        {
            var objectIdLengthConstraint = 24;

            RuleFor(command => command.Id)
                .NotEmpty()
                .Length(objectIdLengthConstraint);
        }
    }
}
