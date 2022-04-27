namespace DynamicEntity.Abstractions
{
    public interface IDynamicEntityPropertyService
    {
        Type GetDynamicEntityTypeBuilder(AssemblyBuilder assemblyBuilder,
            DynamicEntityModel dynamicEntity, string uniqueIdentifier);
    }
}
