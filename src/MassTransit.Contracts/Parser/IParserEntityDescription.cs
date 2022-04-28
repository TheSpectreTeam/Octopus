namespace MassTransit.Contracts
{
    public interface IParserEntityDescription
    {
        Guid Id { get; set; }

        string EntityType { get; set; }

        string EntityFilePath { get; set; }
    }
}
