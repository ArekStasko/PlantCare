namespace PlantCare.Commands.Abstraction.Commands.Module;

public record UpdateModuleCommand : IHttpPostCommand
{
    public Guid Id { get; set; }
};