using System.Reflection;
using System.ComponentModel;
using System.Reflection.Emit;
using DynamicEntity.Extensions;
using Common.Models.DynamicEntity;
using DynamicEntity.Abstractions;

namespace DynamicEntity.Services
{
    public class DynamicTypeFactory : IDynamicTypeFactory
    {
        private TypeBuilder _typeBuilder;

        private readonly AssemblyBuilder _assemblyBuilder;
        private readonly ModuleBuilder _moduleBuilder;

        public DynamicTypeFactory()
        {
            var uniqueIdentifier = Guid.NewGuid().ToString();
            var assemblyName = new AssemblyName(uniqueIdentifier);

            _assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndCollect);
            _moduleBuilder = _assemblyBuilder.DefineDynamicModule(uniqueIdentifier);
        }

        private Type CreateNewTypeWithDynamicProperty(Type parentType,
            string typeName,
            IEnumerable<DynamicEntityModelProperty> dynamicProperties)
        {
            _typeBuilder = _moduleBuilder.DefineType(typeName, TypeAttributes.Public);
            _typeBuilder.SetParent(parentType);

            foreach (var property in dynamicProperties)
                AddDynamicPropertyToType(property);

            return _typeBuilder.CreateType();
        }

        public Type GetTypeWithDynamicProperty(Type parentType, string typeName, IEnumerable<DynamicEntityModelProperty> dynamicProperties)
        {
            var type = _assemblyBuilder.GetType(typeName);
            return type ?? CreateNewTypeWithDynamicProperty(parentType, typeName, dynamicProperties);
        }

        private void AddDynamicPropertyToType(DynamicEntityModelProperty property)
        {
            var propertyType = property.SystemType;
            var propertyName = $"{property.PropertyName.ToCamelCase()}";
            var fieldName = $"_{propertyName.ToLower()}";

            var fieldBuilder = _typeBuilder.DefineField(fieldName, propertyType, FieldAttributes.Public);
            var getSetAttributes = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;

            var getMethodBuilder = _typeBuilder.DefineMethod($"get_{propertyName}", getSetAttributes, propertyType, Type.EmptyTypes);
            var propertyGetGenerator = getMethodBuilder.GetILGenerator();
            propertyGetGenerator.Emit(OpCodes.Ldarg_0);
            propertyGetGenerator.Emit(OpCodes.Ldfld, fieldBuilder);
            propertyGetGenerator.Emit(OpCodes.Ret);

            var setMethodBuilder = _typeBuilder.DefineMethod($"set_{propertyName}", getSetAttributes, null, new[] { propertyType });
            var propertySetGenerator = setMethodBuilder.GetILGenerator();
            propertySetGenerator.Emit(OpCodes.Ldarg_0);
            propertySetGenerator.Emit(OpCodes.Ldarg_1);
            propertySetGenerator.Emit(OpCodes.Stfld, fieldBuilder);
            propertySetGenerator.Emit(OpCodes.Ret);

            var propertyBuilder = _typeBuilder.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);
            propertyBuilder.SetGetMethod(getMethodBuilder);
            propertyBuilder.SetSetMethod(setMethodBuilder);

            var attributeType = typeof(DisplayNameAttribute);
            var attributeBuilder = new CustomAttributeBuilder(
                con: attributeType.GetConstructor(new[] { typeof(string) }),
                constructorArgs: new object[] { property.PropertyName },
                namedProperties: new PropertyInfo[] { },
                propertyValues: new object[] { });
            propertyBuilder.SetCustomAttribute(attributeBuilder);
        }
    }
}
