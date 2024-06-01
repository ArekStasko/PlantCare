namespace PlantCare.Commands.Commands.Module;

public record AddModuleCommand : IHttpPostCommandGuid
{
    public int UserId { get; set; }
}