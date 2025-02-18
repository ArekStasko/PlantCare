namespace PlantCare.Commands.Commands.Module;

public record CreateModuleCommand : IHttpPostCommandId
{
    public int UserId { get; set; }
}