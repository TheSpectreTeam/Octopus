using MongoDB.Driver;

namespace Parser.Core.Application.Commands.ReplaceOneParserDynamicEntityModel
{
    public class ReplaceOneParserDynamicEntityModelCommandHandler
        : IRequestHandler<ReplaceOneParserDynamicEntityModelCommand, Response<ParserDynamicEntityModel>>
    {
        private readonly IMapper _mapper;
        private readonly IMongoRepository<ParserDynamicEntityModel> _mongoRepository;

        public ReplaceOneParserDynamicEntityModelCommandHandler(
            IMapper mapper,
            IMongoRepository<ParserDynamicEntityModel> mongoRepository)
        {
            _mapper = mapper;
            _mongoRepository = mongoRepository;
        }

        public async Task<Response<ParserDynamicEntityModel>> Handle(
            ReplaceOneParserDynamicEntityModelCommand request, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var entity = _mapper.Map<ParserDynamicEntityModel>(request);

            var replaceOptions = new FindOneAndReplaceOptions<ParserDynamicEntityModel, ParserDynamicEntityModel>
            {
                ReturnDocument = ReturnDocument.After
            };

            return await _mongoRepository.ReplaceOneAsync(
                entity: entity,
                options: replaceOptions,
                cancellationToken: cancellationToken) is ParserDynamicEntityModel updatedEntity
                ? new Response<ParserDynamicEntityModel>(
                    data: updatedEntity,
                    message: ResponseMessages.EntitySuccessfullyUpdated)
                : throw new InvalidOperationException(nameof(ReplaceOneParserDynamicEntityModelCommand));
        }
    }
}
