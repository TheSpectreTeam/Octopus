using Parser.Core.Application.Models;

namespace Parser.Core.Application.Commands.DeleteManyParserDynamicEntityModel
{
    public class DeleteManyParserDynamicEntityModelCommandHandler
        : IRequestHandler<DeleteManyParserDynamicEntityModelCommand>
    {
        private readonly IMongoRepository<ParserDynamicEntityModel> _mongoRepository;

        public DeleteManyParserDynamicEntityModelCommandHandler(
            IMongoRepository<ParserDynamicEntityModel> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<Unit> Handle(DeleteManyParserDynamicEntityModelCommand request,
            CancellationToken cancellationToken)
        {
            await _mongoRepository.DeleteManyAsync(request.FilterExpression);

            return Unit.Value;
        }
    }
}
