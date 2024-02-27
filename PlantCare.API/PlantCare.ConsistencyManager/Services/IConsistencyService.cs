using PlantCare.Domain.Dto;

namespace PlantCare.ConsistencyManager.Services;

public interface IConsistencyService
{
    public Task<bool> UpdatePlantData(PlantDto plant);
    public Task<bool> UpdateModuleData(ModuleDto module);
    public Task<bool> UpdatePlaceData(PlaceDto place);
    public Task<bool> UpdateHumidityMeasurementData(HumidityMeasurementDto humidityMeasurement);
}