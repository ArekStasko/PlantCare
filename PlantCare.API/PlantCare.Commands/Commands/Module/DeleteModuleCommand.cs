namespace PlantCare.Commands.Commands.Module;

public record DeleteModuleCommand : IHttpDeleteCommand
{
    public Guid Id { get; set; }
    public int UserId { get; set; }
}