namespace Parser.Core.Application.Commands.ReplaceOneParserDynamicEntityModel
{
    public class ReplaceOneParserDynamicEntityModelCommandHandler
        : IRequestHandler<ReplaceOneParserDynamicEntityModelCommand, ParserDynamicEntityModel>
    {
        private readonly IMongoRepository<ParserDynamicEntityModel> _mongoRepository;

        public ReplaceOneParserDynamicEntityModelCommandHandler(
            IMongoRepository<ParserDynamicEntityModel> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<ParserDynamicEntityModel> Handle(ReplaceOneParserDynamicEntityModelCommand request, 
            CancellationToken cancellationToken)
        {
            return await _mongoRepository.ReplaceOneAsync(
                entity: request.Model,
                cancellationToken: cancellationToken);
        }
    }
}
