namespace DynamicEntity.Models
{
    internal class DynamicEntityPropertyBuilder : DynamicEntityMethodBuilder
    {
        private readonly Type[]? _parameterTypes;
        private readonly PropertyBuilder _propertyBuilder;
        private readonly PropertyAttributes _propertyAttributes;
        
        internal DynamicEntityPropertyBuilder(TypeBuilder typeBuilder, 
            DynamicEntityModelProperty propertyField,
            Type[]? parameterTypes = null,
            PropertyAttributes propertyAttributes = PropertyAttributes.HasDefault) 
            : base(typeBuilder, propertyField) 
        {
            _propertyAttributes = propertyAttributes;
            _parameterTypes = parameterTypes;
            _propertyBuilder = GetPropertyBuilder();
        }

        internal void SetGetMethod()
            => _propertyBuilder
            .SetGetMethod(
                base.GetMethodBuilder(
                methodName: "get",
                parameterTypes: Type.EmptyTypes,
                returnType: base.PropertyField.SystemType)
                .GenerateGetMethodILCode(base.FieldBuilder));

        internal void SetSetMethod()
            => _propertyBuilder
            .SetSetMethod(base.GetMethodBuilder(
                methodName: "set",
                parameterTypes: new[] { base.PropertyField.SystemType })
                .GenerateSetMethodILCode(base.FieldBuilder));

        internal void SetCustomAttribute()
            => _propertyBuilder
            .SetCustomAttribute(CustomAttributeBuilder);

        private PropertyBuilder GetPropertyBuilder() 
            => base.TypeBuilder.DefineProperty(
                name: base.PropertyField.GetValidPropertyName(),
                returnType: base.PropertyField.SystemType,
                attributes: _propertyAttributes,
                parameterTypes: _parameterTypes);
    }
}
