namespace Parser.Core.Application.Queries.GetParserDynamicEntityModelById
{
    public class GetParserDynamicEntityModelByIdQueryHandler 
        : IRequestHandler<GetParserDynamicEntityModelByIdQuery, Response<ParserDynamicEntityModel>>
    {
        private readonly IMongoRepository<ParserDynamicEntityModel> _mongoRepository;

        public GetParserDynamicEntityModelByIdQueryHandler(IMongoRepository<ParserDynamicEntityModel> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<Response<ParserDynamicEntityModel>> Handle(
            GetParserDynamicEntityModelByIdQuery request, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var entity = await _mongoRepository.GetByIdAsync(request.Id);

            return new Response<ParserDynamicEntityModel>(
                data: entity,
                message: ResponseMessages.EntitySuccessfullyFinded);
        }
    }
}
