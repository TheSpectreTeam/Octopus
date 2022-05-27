namespace Parser.Core.Application.Commands.DeleteParserDynamicEntityModelById
{
    public class DeleteParserDynamicEntityModelByIdCommand : IRequest<Response<bool>>
    {
        public string Id { get; set; }
    }
}
