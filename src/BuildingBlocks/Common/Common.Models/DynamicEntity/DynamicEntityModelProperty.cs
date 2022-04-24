using System.Text.Json.Serialization;

namespace Common.Models.DynamicEntity
{
    public class DynamicEntityModelProperty
    {
        public string PropertyName { get; set; }
        public string SystemTypeName { get; set; }
        public int ValueIndex { get; set; }

        [JsonIgnore]
        public Type SystemType => Type.GetType(SystemTypeName);

        public DynamicEntityDatabaseProperty DatabaseEntityProperty { get; set; }
    }
}
