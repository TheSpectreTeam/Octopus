using Common.Models.DynamicEntity;

namespace DynamicEntity.Abstractions
{
    public interface IDynamicTypeFactory
    {
        Type GetTypeWithDynamicProperty(DynamicEntityModel dynamicEntity);
    }
}