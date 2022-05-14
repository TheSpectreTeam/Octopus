using Repository.MongoDb.Abstractions;

namespace Loader.Core.Application.Features.DynamicEntity.Commands.CreateDynamicEntity
{
    internal class CreateDynamicEntityCommandHandler : IRequestHandler<CreateDynamicEntityCommand>
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

        public async Task<Unit> Handle(CreateDynamicEntityCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<LoaderDynamicEntity>(request);
            await _mongoRepository.CreateAsync(entity);
            return Unit.Value;
        }
    }
}
