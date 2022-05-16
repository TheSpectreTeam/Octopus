namespace Loader.Core.Application.Features.DynamicEntity.Commands.UpdateDynamicEntity
{
    public class UpdateDynamicEntityCommandHandler
        : IRequestHandler<UpdateDynamicEntityCommand, Response<LoaderDynamicEntity>>
    {
        private readonly IMapper _mapper;
        private readonly IMongoRepository<LoaderDynamicEntity> _mongoRepository;

        public UpdateDynamicEntityCommandHandler(IMapper mapper, 
            IMongoRepository<LoaderDynamicEntity> mongoRepository)
        {
            _mapper = mapper;
            _mongoRepository = mongoRepository;
        }

        public async Task<Response<LoaderDynamicEntity>> Handle(UpdateDynamicEntityCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<LoaderDynamicEntity>(request);
            var resultEntity = await _mongoRepository.ReplaceOneAsync(entity);
            return new Response<LoaderDynamicEntity>(
                data: resultEntity,
                message: ResponseMessages.EntitySuccessfullyUpdated);
        }
    }
}
