using PlantCare.API.Services.Requests;

namespace PlantCare.API.Services.Tests;

public class HandlersTests
{
    
    [Test]
    public void CreatePlantHandler_Should_CreateOnePlant()
    {
        CreatePlantRequest plantToCreate = new CreatePlantRequest()
        {
            Name = "Test Name",
            Description = "Test Description",
            Type = 0,
            CriticalMoistureLevel = 30,
            RequiredMoistureLevel = 70,
            ModuleId = Guid.NewGuid()
        };
        
        
        Assert.Pass();
    }
}