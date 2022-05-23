namespace Parser.Core.Application.Commands.CreateParserDynamicEntityModel
{
    public class CreateParserDynamicEntityModelCommandHandler
        : IRequestHandler<CreateParserDynamicEntityModelCommand>
    {
        private readonly IMongoRepository<ParserDynamicEntityModel> _mongoRepository;

        public CreateParserDynamicEntityModelCommandHandler(
            IMongoRepository<ParserDynamicEntityModel> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<Unit> Handle(CreateParserDynamicEntityModelCommand request, 
            CancellationToken cancellationToken)
        {
            await _mongoRepository.CreateAsync(
                entity: request.Model,
                cancellationToken: cancellationToken);

            return Unit.Value;
        }
    }
}
