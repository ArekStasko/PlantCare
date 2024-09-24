using PlantCare.Commands.Commands.Module;

namespace PlantCare.Tests.Commands;

public class ModuleCommandHandlerTests
{

    [Fact]
    public async void AddModuleHandler_Should_ReturnSuccess()
    {
        var command = new AddModuleCommand()
        {
            UserId = 1
        };
        
        
    }
}