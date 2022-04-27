namespace DynamicEntity.Models
{
    internal class DynamicEntityPropertyBuilder : DynamicEntityMethodBuilder
    {
        internal PropertyBuilder PropertyBuilder { get; }
        private readonly PropertyAttributes _propertyAttributes;
        private readonly Type[]? _parameterTypes;

        internal DynamicEntityPropertyBuilder(TypeBuilder typeBuilder, 
            DynamicEntityModelProperty propertyField,
            Type[]? parameterTypes = null,
            PropertyAttributes propertyAttributes = PropertyAttributes.HasDefault) 
            : base(typeBuilder, propertyField) 
        {
            _propertyAttributes = propertyAttributes;
            _parameterTypes = parameterTypes;
            PropertyBuilder = GetPropertyBuilder();
        }

        internal void SetGetMethod()
            => PropertyBuilder
            .SetGetMethod(
                base.GetMethodBuilder(
                methodName: "get",
                parameterTypes: Type.EmptyTypes,
                returnType: base.PropertyField.SystemType)
                .GenerateGetMethodILCode(base.FieldBuilder));

        internal void SetSetMethod()
            => PropertyBuilder
            .SetSetMethod(base.GetMethodBuilder(
                methodName: "set",
                parameterTypes: new[] { base.PropertyField.SystemType })
                .GenerateSetMethodILCode(base.FieldBuilder));

        internal void SetCustomAttribute()
            => PropertyBuilder
            .SetCustomAttribute(CustomAttributeBuilder);

        private PropertyBuilder GetPropertyBuilder() 
            => base.TypeBuilder.DefineProperty(
                name: base.PropertyField.GetValidPropertyName(),
                returnType: base.PropertyField.SystemType,
                attributes: _propertyAttributes,
                parameterTypes: _parameterTypes);
    }
}
