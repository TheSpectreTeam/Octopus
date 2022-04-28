namespace MassTransit.Contracts.Loader
{
    public interface ILoaderEntityDescription
    {
        Guid Id { get; set; }

        string EntityType { get; set; }

        string EntityFilePath { get; set; }
    }
}
