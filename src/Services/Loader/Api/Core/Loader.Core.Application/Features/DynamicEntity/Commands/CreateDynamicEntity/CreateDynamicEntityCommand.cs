namespace Loader.Core.Application.Features.DynamicEntity.Commands.CreateDynamicEntity
{
    public class CreateDynamicEntityCommand : IRequest<Response<string>>
    {
        public string EntityName { get; set; }
        public IEnumerable<DynamicEntityModelProperty> Properties { get; set; }
    }
}