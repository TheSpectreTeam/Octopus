namespace Loader.Presentation.WebApi.Models
{
    public class LoaderDynamicEntityModel : DynamicEntityModel, IMongoEntityBase
    {
        public ObjectId Id { get; set; }
        public DateTime CreateAt => Id.CreationTime;
    }
}
