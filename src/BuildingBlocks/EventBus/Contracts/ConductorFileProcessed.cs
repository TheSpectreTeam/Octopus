namespace Contracts
{
    public interface ConductorFileProcessed
    {
        Guid Id { get; set; }

        string EntityType { get; set; }

        string EntityFilePath { get; set; }
    }
}
