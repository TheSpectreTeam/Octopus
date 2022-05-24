namespace Loader.Core.Application.Features.DynamicEntity.Commands.DeleteDynamicEntity
{
    public class DeleteDynamicEntityCommandHandler
        : IRequestHandler<DeleteDynamicEntityCommand, Response<bool>>
    {
        private readonly IMongoRepository<LoaderDynamicEntity> _mongoRepository;

        public DeleteDynamicEntityCommandHandler(IMongoRepository<LoaderDynamicEntity> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<Response<bool>> Handle(
            DeleteDynamicEntityCommand request,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _mongoRepository
                .DeleteByIdAsync(request.Id) is bool isDeleted
                ? new Response<bool>(
                    data: isDeleted,
                    message: ResponseMessages.EntitySuccessfullyDeleted)
                : throw new InvalidOperationException(nameof(DeleteDynamicEntityCommand));
        }
    }
}
