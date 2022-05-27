namespace Parser.Core.Application.Commands.DeleteParserDynamicEntityModelById
{
    public class DeleteParserDynamicEntityModelByIdCommandHandler
        : IRequestHandler<DeleteParserDynamicEntityModelByIdCommand, Response<bool>>
    {
        private readonly IMongoRepository<ParserDynamicEntityModel> _mongoRepository;

        public DeleteParserDynamicEntityModelByIdCommandHandler(
            IMongoRepository<ParserDynamicEntityModel> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<Response<bool>> Handle(
            DeleteParserDynamicEntityModelByIdCommand request, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _mongoRepository.DeleteByIdAsync(
                id: request.Id,
                cancellationToken: cancellationToken) is bool isDeleted
                ? new Response<bool>(
                    data: isDeleted,
                    message: ResponseMessages.EntitySuccessfullyDeleted)
                : throw new InvalidOperationException(nameof(DeleteParserDynamicEntityModelByIdCommand));
        }
    }
}
