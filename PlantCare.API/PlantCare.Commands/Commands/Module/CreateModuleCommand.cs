namespace PlantCare.Commands.Commands.Module;

public record CreateModuleCommand : IHttpPostCommand
{
    public Guid Id { get; set; }
    public int UserId { get; set; }
}