namespace Loader.Core.Application.Features.DynamicEntity.Commands.UpdateDynamicEntity
{
    public class UpdateDynamicEntityCommand : IRequest<LoaderDynamicEntity>
    {
        public string Id { get; set; }
        public string EntityName { get; set; }
        public IEnumerable<DynamicEntityModelProperty> Properties { get; set; }
    }
}
