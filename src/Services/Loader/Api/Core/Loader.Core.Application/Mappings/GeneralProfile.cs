using Loader.Core.Application.Features.DynamicEntity.Commands.CreateDynamicEntity;
using Loader.Core.Application.Features.DynamicEntity.Commands.UpdateDynamicEntity;

namespace Loader.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<CreateDynamicEntityCommand, LoaderDynamicEntity>();
            CreateMap<UpdateDynamicEntityCommand, LoaderDynamicEntity>();
        }
    }
}
