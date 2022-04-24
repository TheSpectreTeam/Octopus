using DynamicEntity.Models;

namespace DynamicEntity.Services
{
    public class DynamicEntityPropertyService : IDynamicEntityPropertyService
    {
        private readonly Type _parentType;

        public DynamicEntityPropertyService()
        {
            _parentType = typeof(DynamicEntityModelBase);
        }

        public Type GetDynamicEntityTypeBuilder(AssemblyBuilder assemblyBuilder, DynamicEntityModel dynamicEntity, string uniqueIdentifier)
        {
            var moduleBuilder = assemblyBuilder.DefineDynamicModule(uniqueIdentifier);
            var typeBuilder = moduleBuilder
                .DefineType(
                name: dynamicEntity.EntityName,
                attr: TypeAttributes.Public);
            typeBuilder
                .SetParent(_parentType);

            dynamicEntity
                .Properties
                .ToList()
                .ForEach(property => AddDynamicPropertyToType(
                    typeBuilder: typeBuilder,
                    propertyField: property));

            return typeBuilder.CreateType();
        }


        private void AddDynamicPropertyToType(TypeBuilder typeBuilder, DynamicEntityModelProperty propertyField)
        {
            var propertyBuilderNew = new DynamicEntityPropertyBuilder(typeBuilder, propertyField);
            propertyBuilderNew.SetGetMethod();
            propertyBuilderNew.SetSetMethod();
            propertyBuilderNew.SetCustomAttribute();
        }
    }
}
