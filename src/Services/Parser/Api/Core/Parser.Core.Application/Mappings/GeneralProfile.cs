using Parser.Core.Application.Commands.CreateParserDynamicEntityModel;

namespace Parser.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<CreateParserDynamicEntityModelCommand, ParserDynamicEntityModel>();
        }
    }
}
