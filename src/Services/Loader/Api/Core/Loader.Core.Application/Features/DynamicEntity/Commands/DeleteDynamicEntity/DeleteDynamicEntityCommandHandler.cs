using Repository.MongoDb.Abstractions;

namespace Loader.Core.Application.Features.DynamicEntity.Commands.DeleteDynamicEntity
{
    public class DeleteDynamicEntityCommandHandler : IRequestHandler<DeleteDynamicEntityCommand>
    {
        private readonly IMongoRepository<LoaderDynamicEntity> _mongoRepository;

        public DeleteDynamicEntityCommandHandler(IMongoRepository<LoaderDynamicEntity> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<Unit> Handle(DeleteDynamicEntityCommand request, CancellationToken cancellationToken)
        {
            await _mongoRepository.DeleteByIdAsync(request.Id);
            return Unit.Value;
        }
    }
}
