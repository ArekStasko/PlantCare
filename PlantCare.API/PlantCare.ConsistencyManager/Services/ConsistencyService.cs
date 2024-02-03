using PlantCare.Domain.Dto;

namespace PlantCare.ConsistencyManager.Services;

public class ConsistencyService : IConsistencyService
{
    public Task<bool> UpdatePlantData(PlantDto plant)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateModuleData(ModuleDto module)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdatePlaceData(PlaceDto place)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateHumidityMeasurementData(HumidityMeasurementDto humidityMeasurement)
    {
        throw new NotImplementedException();
    }
}