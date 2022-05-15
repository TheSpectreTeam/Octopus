using Repository.MongoDb.Abstractions;

namespace Loader.Core.Application.Features.DynamicEntity.Commands.UpdateDynamicEntity
{
    public class UpdateDynamicEntityCommandHandler
        : IRequestHandler<UpdateDynamicEntityCommand, LoaderDynamicEntity>
    {
        private readonly IMapper _mapper;
        private readonly IMongoRepository<LoaderDynamicEntity> _mongoRepository;

        public UpdateDynamicEntityCommandHandler(IMapper mapper, 
            IMongoRepository<LoaderDynamicEntity> mongoRepository)
        {
            _mapper = mapper;
            _mongoRepository = mongoRepository;
        }

        public async Task<LoaderDynamicEntity> Handle(UpdateDynamicEntityCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<LoaderDynamicEntity>(request);
            return await _mongoRepository.ReplaceOneAsync(entity);
        }
    }
}
