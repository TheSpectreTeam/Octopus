namespace Parser.Core.Application.Commands.DeleteParserDynamicEntityModelById
{
    public class DeleteParserDynamicEntityModelByIdCommand : IRequest
    {
        public object Id { get; set; }
    }
}
