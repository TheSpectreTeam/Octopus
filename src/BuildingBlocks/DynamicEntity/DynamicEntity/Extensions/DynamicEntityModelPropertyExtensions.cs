namespace DynamicEntity.Extensions
{
    public static class DynamicEntityModelPropertyExtensions
    {
        public static string GetValidPropertyName(this DynamicEntityModelProperty dynamicEntityModelProperty)
            => dynamicEntityModelProperty.PropertyName.CapitalizedString();

        public static string GetValidFieldName(this DynamicEntityModelProperty dynamicEntityModelProperty)
            => $"_{dynamicEntityModelProperty.PropertyName.ToLower()}";
    }
}
