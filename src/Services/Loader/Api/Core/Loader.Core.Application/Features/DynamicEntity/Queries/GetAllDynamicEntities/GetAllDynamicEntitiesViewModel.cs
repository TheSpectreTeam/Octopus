namespace Loader.Core.Application.Features.DynamicEntity.Queries.GetAllDynamicEntities
{
    public class GetAllDynamicEntitiesViewModel
    {
        public string Id { get; set; }
        public string EntityName { get; set; }
        public IEnumerable<DynamicEntityModelProperty> Properties { get; set; }
    }
}
