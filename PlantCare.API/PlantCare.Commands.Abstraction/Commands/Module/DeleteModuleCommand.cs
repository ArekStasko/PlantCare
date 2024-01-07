namespace PlantCare.Commands.Abstraction.Commands.Module;

public record DeleteModuleCommand : IHttpDeleteCommand
{
    public Guid Id { get; set; }
}