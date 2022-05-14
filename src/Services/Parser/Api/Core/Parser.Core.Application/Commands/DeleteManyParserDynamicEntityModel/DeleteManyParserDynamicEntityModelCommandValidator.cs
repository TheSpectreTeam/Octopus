namespace Parser.Core.Application.Commands.DeleteManyParserDynamicEntityModel
{
    public class DeleteManyParserDynamicEntityModelCommandValidator
        : AbstractValidator<DeleteManyParserDynamicEntityModelCommand>
    {
        public DeleteManyParserDynamicEntityModelCommandValidator()
        {
            RuleFor(command => command.FilterExpression).NotEmpty();
        }
    }
}
