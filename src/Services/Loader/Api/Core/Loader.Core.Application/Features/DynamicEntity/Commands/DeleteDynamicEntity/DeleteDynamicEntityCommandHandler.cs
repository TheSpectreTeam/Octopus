namespace Loader.Core.Application.Features.DynamicEntity.Commands.DeleteDynamicEntity
{
    public class DeleteDynamicEntityCommandHandler 
        : IRequestHandler<DeleteDynamicEntityCommand, Response<Unit>>
    {
        private readonly IMongoRepository<LoaderDynamicEntity> _mongoRepository;

        public DeleteDynamicEntityCommandHandler(IMongoRepository<LoaderDynamicEntity> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<Response<Unit>> Handle(DeleteDynamicEntityCommand request, CancellationToken cancellationToken)
        {
            await _mongoRepository.DeleteByIdAsync(request.Id);
            return new Response<Unit>(
                data: Unit.Value, 
                message: ResponseMessages.EntitySuccessfullyDeleted);
        }
    }
}
