namespace Parser.Core.Application.Commands.DeleteParserDynamicEntityModelById
{
    public class DeleteParserDynamicEntityModelByIdCommandHandler
        : IRequestHandler<DeleteParserDynamicEntityModelByIdCommand>
    {
        private readonly IMongoRepository<ParserDynamicEntityModel> _mongoRepository;

        public DeleteParserDynamicEntityModelByIdCommandHandler(
            IMongoRepository<ParserDynamicEntityModel> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<Unit> Handle(DeleteParserDynamicEntityModelByIdCommand request, 
            CancellationToken cancellationToken)
        {
            await _mongoRepository.DeleteByIdAsync(
                id: request.Id,
                cancellationToken: cancellationToken);

            return Unit.Value;
        }
    }
}
