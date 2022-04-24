namespace DynamicEntity.Models
{
    internal class DynamicEntityBuilderBase
    {
        internal TypeBuilder TypeBuilder { get; set; }
        internal DynamicEntityModelProperty PropertyField { get; set; }

        internal DynamicEntityBuilderBase(TypeBuilder typeBuilder, DynamicEntityModelProperty propertyField)
        {
            this.TypeBuilder = typeBuilder;
            this.PropertyField = propertyField;
        }
    }
}
