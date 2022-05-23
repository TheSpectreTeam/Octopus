namespace Loader.Core.Application.Features.DynamicEntity.Commands.DeleteDynamicEntity
{
    public class DeleteDynamicEntityCommand : IRequest<Response<bool>>
    {
        public string Id { get; set; }
    }
}
