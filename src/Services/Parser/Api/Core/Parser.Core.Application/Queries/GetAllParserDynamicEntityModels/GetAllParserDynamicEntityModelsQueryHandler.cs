namespace Parser.Core.Application.Queries.GetAllParserDynamicEntityModels
{
    public class GetAllParserDynamicEntityModelsQueryHandler 
        : IRequestHandler<GetAllParserDynamicEntityModelsQuery, Response<IReadOnlyList<ParserDynamicEntityModel>>>
    {
        private readonly IMongoRepository<ParserDynamicEntityModel> _mongoRepository;

        public GetAllParserDynamicEntityModelsQueryHandler(IMongoRepository<ParserDynamicEntityModel> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<Response<IReadOnlyList<ParserDynamicEntityModel>>> Handle(
            GetAllParserDynamicEntityModelsQuery request, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var entities = await _mongoRepository.GetAllAsync();

            return new Response<IReadOnlyList<ParserDynamicEntityModel>>(
                data: entities,
                message: ResponseMessages.EntitiesSuccessfullyFinded);
        }
    }
}
