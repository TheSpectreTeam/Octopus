namespace MassTransit.Contracts.Conductor
{
    public interface IConductorEntityDescription
    {
        Guid Id { get; set; }

        string EntityType { get; set; }

        string InputDirectory { get; set; }

        string OutputDirectory { get; set; }
    }
}
