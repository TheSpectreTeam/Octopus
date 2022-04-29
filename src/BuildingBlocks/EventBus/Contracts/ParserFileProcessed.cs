namespace Contracts
{
    public interface ParserFileProcessed
    {
        Guid Id { get; set; }

        string EntityType { get; set; }

        string EntityFilePath { get; set; }
    }
}
