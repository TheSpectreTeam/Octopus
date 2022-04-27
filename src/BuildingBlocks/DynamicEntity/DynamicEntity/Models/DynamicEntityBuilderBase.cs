namespace DynamicEntity.Models
{
    internal class DynamicEntityBuilderBase
    {
        protected TypeBuilder TypeBuilder { get; set; }
        protected DynamicEntityModelProperty PropertyField { get; set; }

        internal DynamicEntityBuilderBase(TypeBuilder typeBuilder, DynamicEntityModelProperty propertyField)
        {
            this.TypeBuilder = typeBuilder;
            this.PropertyField = propertyField;
        }
    }
}
