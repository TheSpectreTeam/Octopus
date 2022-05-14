namespace Parser.Core.Application.Commands.DeleteOneParserDynamicEntityModel
{
    public class DeleteOneParserDynamicEntityModelCommandValidator
        : AbstractValidator<DeleteOneParserDynamicEntityModelCommand>
    {
        public DeleteOneParserDynamicEntityModelCommandValidator()
        {
            RuleFor(command => command.FilterExpression).NotEmpty();
        }
    }
}
