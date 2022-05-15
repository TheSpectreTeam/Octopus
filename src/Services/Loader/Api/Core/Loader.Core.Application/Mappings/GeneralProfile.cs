using Loader.Core.Application.Features.DynamicEntity.Commands.CreateDynamicEntity;
using Loader.Core.Application.Features.DynamicEntity.Commands.UpdateDynamicEntity;
using Loader.Core.Application.Features.DynamicEntity.Queries.GetAllDynamicEntities;

namespace Loader.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<CreateDynamicEntityCommand, LoaderDynamicEntity>();
            CreateMap<LoaderDynamicEntity, GetAllDynamicEntitiesViewModel>();
            CreateMap<UpdateDynamicEntityCommand, LoaderDynamicEntity>();
        }
    }
}
