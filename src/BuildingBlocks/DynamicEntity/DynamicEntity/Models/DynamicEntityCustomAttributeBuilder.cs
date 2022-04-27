using System.ComponentModel;

namespace DynamicEntity.Models
{
    internal class DynamicEntityCustomAttributeBuilder : DynamicEntityBuilderBase
    {
        private readonly Type _attributeType = typeof(DisplayNameAttribute);
        internal CustomAttributeBuilder CustomAttributeBuilder => GetCustomAttributeBuilder();

        internal DynamicEntityCustomAttributeBuilder(TypeBuilder typeBuilder,
            DynamicEntityModelProperty propertyField)
            : base(typeBuilder, propertyField) { }

        private CustomAttributeBuilder GetCustomAttributeBuilder()
            => new CustomAttributeBuilder(
                con: _attributeType.GetConstructor(new[] { typeof(string) }),
                constructorArgs: new object[] { base.PropertyField.GetValidPropertyName() },
                namedProperties: new PropertyInfo[] { },
                propertyValues: new object[] { });
    }
}
