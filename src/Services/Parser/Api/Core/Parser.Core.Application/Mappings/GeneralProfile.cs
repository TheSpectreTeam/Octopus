namespace Parser.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<CreateParserDynamicEntityModelCommand, ParserDynamicEntityModel>();
            CreateMap<CreateManyParserDynamicEntityModelsCommand, List<ParserDynamicEntityModel>>();
            CreateMap<ReplaceOneParserDynamicEntityModelCommand, ParserDynamicEntityModel>();
        }
    }
}
