namespace PlantCare.Commands.Commands.Module;

public record CreateModuleCommand : IHttpPostCommand
{
    public int Id { get; set; }
    public int UserId { get; set; }
}