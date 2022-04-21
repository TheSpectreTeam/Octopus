using Common.Models.DynamicEntity;

namespace DynamicEntity.Abstractions
{
    public interface IDynamicTypeFactory
    {
        Type GetTypeWithDynamicProperty(Type parentType, string typeName, IEnumerable<DynamicEntityModelProperty> dynamicProperties);
    }
}
