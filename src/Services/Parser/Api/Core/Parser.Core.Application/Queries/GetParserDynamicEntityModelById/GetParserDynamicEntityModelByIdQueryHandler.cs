namespace Parser.Core.Application.Queries.GetParserDynamicEntityModelById
{
    public class GetParserDynamicEntityModelByIdQueryHandler 
        : IRequestHandler<GetParserDynamicEntityModelByIdQuery, ParserDynamicEntityModel>
    {
        private readonly IMongoRepository<ParserDynamicEntityModel> _mongoRepository;

        public GetParserDynamicEntityModelByIdQueryHandler(IMongoRepository<ParserDynamicEntityModel> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<ParserDynamicEntityModel> Handle(GetParserDynamicEntityModelByIdQuery request, CancellationToken cancellationToken)
        {
            return await _mongoRepository.GetByIdAsync(request.Id);
        }
    }
}
