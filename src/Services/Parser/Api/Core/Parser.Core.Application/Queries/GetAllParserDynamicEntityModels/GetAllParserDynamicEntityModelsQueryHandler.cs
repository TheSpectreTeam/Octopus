using Parser.Core.Application.Models;

namespace Parser.Core.Application.Queries.GetAllParserDynamicEntityModels
{
    public class GetAllParserDynamicEntityModelsQueryHandler 
        : IRequestHandler<GetAllParserDynamicEntityModelsQuery, IEnumerable<ParserDynamicEntityModel>>
    {
        private readonly IMongoRepository<ParserDynamicEntityModel> _mongoRepository;

        public GetAllParserDynamicEntityModelsQueryHandler(IMongoRepository<ParserDynamicEntityModel> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<IEnumerable<ParserDynamicEntityModel>> Handle(GetAllParserDynamicEntityModelsQuery request, CancellationToken cancellationToken)
        {
            return await _mongoRepository.GetAllAsync();
        }
    }
}
