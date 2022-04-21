namespace Common.Models.DynamicEntity
{
    public class DynamicEntityModel
    {
        public string EntityName { get; set; }
        public IEnumerable<DynamicEntityModelProperty> Properties { get; set; }
    }
}
