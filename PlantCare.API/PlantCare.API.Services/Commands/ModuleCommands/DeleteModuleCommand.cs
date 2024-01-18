namespace PlantCare.API.Services.Requests.ModuleCommands;

public class DeleteModuleCommand : IHttpDeleteCommand
{
    public Guid Id { get; set; }
}