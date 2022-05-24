namespace Parser.Core.Application.Queries.GetAllParserDynamicEntityModels
{
    public class GetAllParserDynamicEntityModelsQueryHandler 
        : IRequestHandler<GetAllParserDynamicEntityModelsQuery, Response<IEnumerable<ParserDynamicEntityModel>>>
    {
        private readonly IMongoRepository<ParserDynamicEntityModel> _mongoRepository;

        public GetAllParserDynamicEntityModelsQueryHandler(IMongoRepository<ParserDynamicEntityModel> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<Response<IEnumerable<ParserDynamicEntityModel>>> Handle(
            GetAllParserDynamicEntityModelsQuery request, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var entities = await _mongoRepository.GetAllAsync();

            return new Response<IEnumerable<ParserDynamicEntityModel>>(
                data: entities,
                message: ResponseMessages.EntitiesSuccessfullyFinded);
        }
    }
}
