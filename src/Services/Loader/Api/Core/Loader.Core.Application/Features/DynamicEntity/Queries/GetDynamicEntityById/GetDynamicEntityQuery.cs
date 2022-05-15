namespace Loader.Core.Application.Features.DynamicEntity.Queries.GetDynamicEntityById
{
    public class GetDynamicEntityQuery : IRequest<LoaderDynamicEntity>
    {
        public string Id { get; set; }
    }
}
