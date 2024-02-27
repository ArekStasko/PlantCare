using LanguageExt.Common;
using PlantCare.Domain.Models.Plant;

namespace PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;

public interface IWritePlantRepository
{
    ValueTask<Result<int>> Create(IPlant plant);
    ValueTask<Result<bool>> Delete(int id);
    ValueTask<Result<bool>> Update(IPlant plant);
}