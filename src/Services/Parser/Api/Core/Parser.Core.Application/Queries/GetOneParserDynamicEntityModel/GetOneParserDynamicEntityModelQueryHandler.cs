using Parser.Core.Application.Models;

namespace Parser.Core.Application.Queries.GetOneParserDynamicEntityModel
{
    public class GetOneParserDynamicEntityModelQueryHandler
        : IRequestHandler<GetOneParserDynamicEntityModelQuery, ParserDynamicEntityModel>
    {
        private readonly IMongoRepository<ParserDynamicEntityModel> _mongoRepository;

        public GetOneParserDynamicEntityModelQueryHandler(IMongoRepository<ParserDynamicEntityModel> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<ParserDynamicEntityModel> Handle(GetOneParserDynamicEntityModelQuery request, CancellationToken cancellationToken)
        {
            return await _mongoRepository.GetOneAsync(request.FilterExpression);
        }
    }
}
