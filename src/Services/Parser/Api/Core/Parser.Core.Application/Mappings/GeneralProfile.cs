using Parser.Core.Application.Commands.CreateManyParserDynamicEntityModels;
using Parser.Core.Application.Commands.CreateParserDynamicEntityModel;
using Parser.Core.Application.Commands.ReplaceOneParserDynamicEntityModel;

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
