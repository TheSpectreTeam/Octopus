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

        public async Task<Response<LoaderDynamicEntity>> Handle(
            UpdateDynamicEntityCommand request,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            Guard.Against.Null(request, nameof(request));
            var entity = _mapper.Map<LoaderDynamicEntity>(request);
            var replaceOptions = new FindOneAndReplaceOptions<LoaderDynamicEntity, LoaderDynamicEntity> 
            { 
                ReturnDocument = ReturnDocument.After 
            };
            return await _mongoRepository.ReplaceOneAsync(
                entity: entity,
                options: replaceOptions) is LoaderDynamicEntity updatedEntity
                ? new Response<LoaderDynamicEntity>(
                    data: updatedEntity,
                    message: ResponseMessages.EntitySuccessfullyUpdated)
                : throw new InvalidOperationException(nameof(UpdateDynamicEntityCommand));
        }
    }
}
