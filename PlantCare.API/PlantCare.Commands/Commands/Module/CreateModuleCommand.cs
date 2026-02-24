namespace PlantCare.Commands.Commands.Module;

public record CreateModuleCommand : IHttpPostCommandId
{
    public string Name { get; init; }
    public int UserId { get; set; }
}