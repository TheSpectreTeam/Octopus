namespace Parser.Core.Application.Commands.CreateParserDynamicEntityModel
{
    public class CreateParserDynamicEntityModelCommandHandler
        : IRequestHandler<CreateParserDynamicEntityModelCommand, Response<string>>
    {
        private readonly IMapper _mapper;
        private readonly IMongoRepository<ParserDynamicEntityModel> _mongoRepository;

        public CreateParserDynamicEntityModelCommandHandler(
            IMapper mapper,
            IMongoRepository<ParserDynamicEntityModel> mongoRepository)
        {
            _mapper = mapper;
            _mongoRepository = mongoRepository;
        }

        public async Task<Response<string>> Handle(
            CreateParserDynamicEntityModelCommand request, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var entity = _mapper.Map<ParserDynamicEntityModel>(request);

            return await _mongoRepository.CreateAsync(
                entity: entity,
                cancellationToken: cancellationToken) is string stringId
                ? new Response<string>(
                    data: stringId,
                    message: ResponseMessages.EntitySuccessfullyCreated)
                : throw new InvalidOperationException(nameof(CreateParserDynamicEntityModelCommand));
        }
    }
}
