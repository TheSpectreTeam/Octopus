namespace Parser.Core.Application.Commands.DeleteOneParserDynamicEntityModel
{
    public class DeleteOneParserDynamicEntityModelCommandHandler
        : IRequestHandler<DeleteOneParserDynamicEntityModelCommand>
    {
        private readonly IMongoRepository<ParserDynamicEntityModel> _mongoRepository;

        public DeleteOneParserDynamicEntityModelCommandHandler(
            IMongoRepository<ParserDynamicEntityModel> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<Unit> Handle(DeleteOneParserDynamicEntityModelCommand request, 
            CancellationToken cancellationToken)
        {
            await _mongoRepository.DeleteOneAsync(request.FilterExpression);

            return Unit.Value;
        }
    }
}
