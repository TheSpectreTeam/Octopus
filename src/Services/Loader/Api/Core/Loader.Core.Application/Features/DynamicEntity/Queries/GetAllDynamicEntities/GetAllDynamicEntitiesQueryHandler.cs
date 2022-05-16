namespace Loader.Core.Application.Features.DynamicEntity.Queries.GetAllDynamicEntities
{
    public class GetAllDynamicEntitiesQueryHandler
        : IRequestHandler<GetAllDynamicEntitiesQuery, Response<IEnumerable<GetAllDynamicEntitiesViewModel>>>
    {
        private readonly IMapper _mapper;
        private readonly IMongoRepository<LoaderDynamicEntity> _mongoRepository;

        public GetAllDynamicEntitiesQueryHandler(
            IMapper mapper,
            IMongoRepository<LoaderDynamicEntity> mongoRepository)
        {
            _mapper = mapper;
            _mongoRepository = mongoRepository;
        } 

        public async Task<Response<IEnumerable<GetAllDynamicEntitiesViewModel>>> Handle(
            GetAllDynamicEntitiesQuery request, 
            CancellationToken cancellationToken)
        {
            var entities = await _mongoRepository.GetAllAsync();
            var resultEntities = _mapper.Map<IEnumerable<GetAllDynamicEntitiesViewModel>>(entities);
            return new Response<IEnumerable<GetAllDynamicEntitiesViewModel>>(data: resultEntities);
        }
    }
}
