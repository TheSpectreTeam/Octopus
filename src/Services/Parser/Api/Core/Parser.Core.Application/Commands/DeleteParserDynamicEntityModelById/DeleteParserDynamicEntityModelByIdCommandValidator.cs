namespace Parser.Core.Application.Commands.DeleteParserDynamicEntityModelById
{
    public class DeleteParserDynamicEntityModelByIdCommandValidator
        : AbstractValidator<DeleteParserDynamicEntityModelByIdCommand>
    {
        public DeleteParserDynamicEntityModelByIdCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
        }
    }
}
