namespace DynamicEntity.Models
{
    internal class DynamicEntityFieldBuilder : DynamicEntityCustomAttributeBuilder
    {
        protected FieldBuilder FieldBuilder { get; }
        private readonly FieldAttributes _fieldAttributes;

        internal DynamicEntityFieldBuilder(TypeBuilder typeBuilder,
            DynamicEntityModelProperty propertyField,
            FieldAttributes fieldAttributes = FieldAttributes.Public)
            : base(typeBuilder, propertyField)
        {
            _fieldAttributes = fieldAttributes;
            FieldBuilder = GetFieldBuilder();
        }

        private FieldBuilder GetFieldBuilder()
            => base.TypeBuilder.DefineField(
                type: base.PropertyField.SystemType,
                fieldName: base.PropertyField.GetValidFieldName(),
                attributes: _fieldAttributes);
    }
}
