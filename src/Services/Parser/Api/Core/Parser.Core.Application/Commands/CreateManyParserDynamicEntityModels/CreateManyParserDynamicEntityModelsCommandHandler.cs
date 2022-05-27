namespace Parser.Core.Application.Commands.CreateManyParserDynamicEntityModels
{
    public class CreateManyParserDynamicEntityModelsCommandHandler
        : IRequestHandler<CreateManyParserDynamicEntityModelsCommand, Response<IDictionary<int, string>>>
    {
        private readonly IMapper _mapper;
        private readonly IMongoRepository<ParserDynamicEntityModel> _mongoRepository;

        public CreateManyParserDynamicEntityModelsCommandHandler(
            IMapper mapper,
            IMongoRepository<ParserDynamicEntityModel> mongoRepository)
        {
            _mapper = mapper;
            _mongoRepository = mongoRepository;
        }

        public async Task<Response<IDictionary<int, string>>> Handle(
            CreateManyParserDynamicEntityModelsCommand request,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var entities = _mapper.Map<List<ParserDynamicEntityModel>>(request.Models);

            var resultFromRepo = await _mongoRepository.CreateManyAsync(
                entities: entities,
                cancellationToken: cancellationToken);

            var responseDict = new Dictionary<int, string>();

            foreach (var item in resultFromRepo)
            {
                if (item.Value is string itemString)
                {
                    responseDict.Add(item.Key, itemString);
                }
                else
                {
                    throw new InvalidOperationException(nameof(CreateManyParserDynamicEntityModelsCommand));
                }
            }

            return new Response<IDictionary<int, string>>(
                data: responseDict,
                message: ResponseMessages.EntitiesSuccessfullyCreated);
        }
    }
}
