namespace Loader.Core.Application.Features.DynamicEntity.Commands.CreateDynamicEntity
{
    internal class CreateDynamicEntityCommandHandler
        : IRequestHandler<CreateDynamicEntityCommand, Response<string>>
    {
        private readonly IMapper _mapper;
        private readonly IMongoRepository<LoaderDynamicEntity> _mongoRepository;

        public CreateDynamicEntityCommandHandler(
            IMapper mapper,
            IMongoRepository<LoaderDynamicEntity> mongoRepository)
        {
            _mapper = mapper;
            _mongoRepository = mongoRepository;
        }

        public async Task<Response<string>> Handle(
            CreateDynamicEntityCommand request,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var entity = _mapper.Map<LoaderDynamicEntity>(request);
            return await _mongoRepository.CreateAsync(entity) is string stringId
                ? new Response<string>(
                    data: stringId,
                    message: ResponseMessages.EntitySuccessfullyCreated)
                : throw new InvalidOperationException(nameof(CreateDynamicEntityCommand));
        }
    }
}