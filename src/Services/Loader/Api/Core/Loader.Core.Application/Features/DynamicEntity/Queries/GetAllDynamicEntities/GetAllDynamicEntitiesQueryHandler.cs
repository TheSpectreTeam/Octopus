using Repository.MongoDb.Abstractions;

namespace Loader.Core.Application.Features.DynamicEntity.Queries.GetAllDynamicEntities
{
    public class GetAllDynamicEntitiesQueryHandler
        : IRequestHandler<GetAllDynamicEntitiesQuery, IEnumerable<GetAllDynamicEntitiesViewModel>>
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

        public async Task<IEnumerable<GetAllDynamicEntitiesViewModel>> Handle(GetAllDynamicEntitiesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _mongoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GetAllDynamicEntitiesViewModel>>(entities);
        }
    }
}
