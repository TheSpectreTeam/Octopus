using Repository.MongoDb.Abstractions;

namespace Loader.Core.Application.Features.DynamicEntity.Queries.GetDynamicEntityById
{
    public class GetDynamicEntityQueryHandler
        : IRequestHandler<GetDynamicEntityQuery, LoaderDynamicEntity>
    {
        private readonly IMongoRepository<LoaderDynamicEntity> _mongoRepository;

        public GetDynamicEntityQueryHandler(IMongoRepository<LoaderDynamicEntity> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<LoaderDynamicEntity> Handle(GetDynamicEntityQuery request, CancellationToken cancellationToken)
        {
            return await _mongoRepository.GetByIdAsync(request.Id);
        }
    }
}
