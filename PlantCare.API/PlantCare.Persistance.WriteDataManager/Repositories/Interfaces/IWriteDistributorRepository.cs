using LanguageExt.Common;

namespace PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;

public interface IWriteDistributorRepository
{
    ValueTask<Result<int>> Add(int userId, string name);
    ValueTask<Result<bool>> AddPlant(int userId, int distributorId, int plantId);
    ValueTask<Result<bool>> RemovePlant(int userId, int distributorId, int plantId);
}