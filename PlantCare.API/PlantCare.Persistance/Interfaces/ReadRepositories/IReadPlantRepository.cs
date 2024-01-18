using LanguageExt.Common;
using PlantCare.Domain.Models.Plant;

namespace PlantCare.Persistance.Interfaces.ReadRepositories;

public interface IReadPlantRepository
{
    ValueTask<Result<IReadOnlyCollection<IPlant>>> Get();
    ValueTask<Result<IPlant>> Get(int id);
}