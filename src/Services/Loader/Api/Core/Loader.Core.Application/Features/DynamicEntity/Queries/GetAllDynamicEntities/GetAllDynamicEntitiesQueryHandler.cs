namespace Loader.Core.Application.Features.DynamicEntity.Queries.GetAllDynamicEntities
{
    public class GetAllDynamicEntitiesQueryHandler
        : IRequestHandler<GetAllDynamicEntitiesQuery, Response<IEnumerable<LoaderDynamicEntity>>>
    {
        private readonly IMongoRepository<LoaderDynamicEntity> _mongoRepository;

        public GetAllDynamicEntitiesQueryHandler(
            IMapper mapper,
            IMongoRepository<LoaderDynamicEntity> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        } 

        public async Task<Response<IEnumerable<LoaderDynamicEntity>>> Handle(
            GetAllDynamicEntitiesQuery request, 
            CancellationToken cancellationToken)
        {
            var entities = await _mongoRepository.GetAllAsync();
            return new Response<IEnumerable<LoaderDynamicEntity>>(data: entities);
        }
    }
}
