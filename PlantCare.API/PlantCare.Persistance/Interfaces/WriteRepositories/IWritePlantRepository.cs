using LanguageExt.Common;
using PlantCare.Domain.Models.Plant;

namespace PlantCare.Persistance.Interfaces.WriteRepositories;

public interface IWritePlantRepository
{
    ValueTask<Result<bool>> Create(IPlant plant);
    ValueTask<Result<bool>> Delete(int id);
    ValueTask<Result<bool>> Update(IPlant plant);
}