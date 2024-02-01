namespace PlantCare.Commands.Commands.Module;

public record UpdateModuleCommand : IHttpPostCommand
{
    public Guid Id { get; set; }
};