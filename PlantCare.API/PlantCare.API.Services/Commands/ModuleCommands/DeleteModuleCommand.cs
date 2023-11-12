namespace PlantCare.API.Services.Requests.ModuleCommands;

public class DeleteModuleCommand : IHttpDeleteCommand
{
    public int Id { get; set; }
}