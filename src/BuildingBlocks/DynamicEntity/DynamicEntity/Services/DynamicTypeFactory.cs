namespace DynamicEntity.Services
{
    public class DynamicTypeFactory : IDynamicTypeFactory
    {
        private readonly string _uniqueIdentifier;
        private readonly AssemblyBuilder _assemblyBuilder;
        private readonly IDynamicEntityPropertyService _dynamicEntityPropertyService;

        public DynamicTypeFactory(IDynamicEntityPropertyService dynamicEntityPropertyService)
        {
            _uniqueIdentifier = Guid.NewGuid().ToString();
            var assemblyName = new AssemblyName(_uniqueIdentifier);

            _assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
                name: assemblyName,
                access: AssemblyBuilderAccess.RunAndCollect);

            _dynamicEntityPropertyService = dynamicEntityPropertyService;
        }

        public Type GetTypeWithDynamicProperty(DynamicEntityModel dynamicEntity)
        {
            var type = _assemblyBuilder.GetType(dynamicEntity.EntityName);
            return type ?? _dynamicEntityPropertyService
                .GetDynamicEntityTypeBuilder(
                    assemblyBuilder: _assemblyBuilder,
                    dynamicEntity: dynamicEntity,
                    uniqueIdentifier: _uniqueIdentifier);
        }
    }
}
