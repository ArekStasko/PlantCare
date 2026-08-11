using LanguageExt.Common;

namespace PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;

public interface IWriteDistributorRepository
{
    ValueTask<Result<int>> Create(int userId, string name);
    ValueTask<Result<bool>> WaterSupply(int userId, int distributorId, int plantId);
}