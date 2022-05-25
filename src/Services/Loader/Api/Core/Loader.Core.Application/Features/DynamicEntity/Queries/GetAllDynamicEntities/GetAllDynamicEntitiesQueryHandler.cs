namespace Loader.Core.Application.Features.DynamicEntity.Queries.GetAllDynamicEntities
{
    public class GetAllDynamicEntitiesQueryHandler
        : IRequestHandler<GetAllDynamicEntitiesQuery, Response<IReadOnlyList<LoaderDynamicEntity>>>
    {
        private readonly IMongoRepository<LoaderDynamicEntity> _mongoRepository;

        public GetAllDynamicEntitiesQueryHandler(
            IMongoRepository<LoaderDynamicEntity> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        } 

        public async Task<Response<IReadOnlyList<LoaderDynamicEntity>>> Handle(
            GetAllDynamicEntitiesQuery request,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var entities = await _mongoRepository.GetAllAsync();
            return new Response<IReadOnlyList<LoaderDynamicEntity>>(
                    data: entities,
                    message: ResponseMessages.EntitiesSuccessfullFinded);
        }
    }
}
