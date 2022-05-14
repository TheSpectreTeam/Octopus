namespace Loader.Core.Application.Features.DynamicEntity.Commands.DeleteDynamicEntity
{
    public class DeleteDynamicEntityCommand : IRequest
    {
        public string Id { get; set; }
    }
}
