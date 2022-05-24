namespace Loader.Core.Application.Features.DynamicEntity.Queries.GetDynamicEntityById
{
    public class GetDynamicEntityQueryHandler
        : IRequestHandler<GetDynamicEntityQuery, Response<LoaderDynamicEntity>>
    {
        private readonly IMongoRepository<LoaderDynamicEntity> _mongoRepository;

        public GetDynamicEntityQueryHandler(IMongoRepository<LoaderDynamicEntity> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<Response<LoaderDynamicEntity>> Handle(
            GetDynamicEntityQuery request, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var entity = await _mongoRepository
                .GetByIdAsync(request.Id);
            return new Response<LoaderDynamicEntity>(
                    data: entity,
                    message: ResponseMessages.EntitySuccessfullFinded);
        }
    }
}
