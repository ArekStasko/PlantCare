namespace PlantCare.Commands.Commands.Module;

public class SetModuleStatusCommand : IHttpPostCommand
{
    public int? UserId { get; set; }
    public int ModuleId { get; set; }
    public bool Status { get; set; }
}