using LanguageExt.Common;
using PlantCare.API.DataAccess.Models;

namespace PlantCare.API.DataAccess.Repositories.PlantRepository;

public interface IPlantRepository
{
    ValueTask<Result<bool>> Create(IPlant plant);
    ValueTask<Result<bool>> Delete(int id);
    ValueTask<Result<bool>> Edit(IPlant plant);
    ValueTask<Result<List<IPlant>>> Get();
    ValueTask<Result<IPlant>> Get(int id);
}