using LanguageExt.Common;
using PlantCare.Domain.Models.Distributor;

namespace PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;

public interface IReadDistributorRepository
{
    public Task<Result<IDistributor?>> GetDistributor(int id, int userId);
    public Task<Result<IReadOnlyCollection<IDistributor>>> GetDistributors(int userId);
    public Task<Result<WaterSupply?>> GetFirstWaterSupplyByDistributorId(int id);
    Task<Result<WaterSupply?>> GetWaterSupplyByDistributorIdAndPlantId(int id, int plantId);
}