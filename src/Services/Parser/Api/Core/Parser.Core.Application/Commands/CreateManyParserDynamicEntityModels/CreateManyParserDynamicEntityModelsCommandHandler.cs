namespace Parser.Core.Application.Commands.CreateManyParserDynamicEntityModels
{
    public class CreateManyParserDynamicEntityModelsCommandHandler
        : IRequestHandler<CreateManyParserDynamicEntityModelsCommand>
    {
        private readonly IMongoRepository<ParserDynamicEntityModel> _mongoRepository;

        public CreateManyParserDynamicEntityModelsCommandHandler(
            IMongoRepository<ParserDynamicEntityModel> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<Unit> Handle(CreateManyParserDynamicEntityModelsCommand request,
            CancellationToken cancellationToken)
        {
            await _mongoRepository.CreateManyAsync(
                entities: request.Models, 
                cancellationToken: cancellationToken);

            return Unit.Value;
        }
    }
}
