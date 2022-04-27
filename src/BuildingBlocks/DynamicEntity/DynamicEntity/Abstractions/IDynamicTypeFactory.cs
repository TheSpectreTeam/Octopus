namespace DynamicEntity.Abstractions
{
    public interface IDynamicTypeFactory
    {
        Type GetTypeWithDynamicProperty(DynamicEntityModel dynamicEntity);
    }
}