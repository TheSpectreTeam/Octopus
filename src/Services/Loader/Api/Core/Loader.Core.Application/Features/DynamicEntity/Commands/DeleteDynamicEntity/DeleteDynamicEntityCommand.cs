namespace Loader.Core.Application.Features.DynamicEntity.Commands.DeleteDynamicEntity
{
    public class DeleteDynamicEntityCommand : IRequest<Response<Unit>>
    {
        public string Id { get; set; }
    }
}
