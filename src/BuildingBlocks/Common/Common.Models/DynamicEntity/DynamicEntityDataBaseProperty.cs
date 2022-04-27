namespace Common.Models.DynamicEntity
{
    public class DynamicEntityDatabaseProperty
    {
        public string DatabaseTypeName { get; set; }

        public int Length { get; set; }

        public bool IsNotNull { get; set; }

        public bool IsKey { get; set; }

        public string? Comment { get; set; }
    }
}
