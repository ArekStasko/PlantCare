using LanguageExt.Common;
using PlantCare.Domain.Models.Plant;

namespace PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;

public interface IReadPlantRepository
{
    ValueTask<Result<IReadOnlyCollection<IPlant>>> Get(int userId);
    ValueTask<Result<IPlant>> Get(int id, int userId);
}