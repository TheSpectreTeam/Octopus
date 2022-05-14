using Loader.Core.Application.Features.DynamicEntity.Commands.CreateDynamicEntity;

namespace Loader.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<CreateDynamicEntityCommand, LoaderDynamicEntity>();
        }
    }
}
