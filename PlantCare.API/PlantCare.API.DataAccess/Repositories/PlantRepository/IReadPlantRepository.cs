using LanguageExt.Common;
using PlantCare.API.DataAccess.Models;

namespace PlantCare.API.DataAccess.Repositories.PlantRepository;

public interface IReadPlantRepository
{
    ValueTask<Result<IReadOnlyCollection<IPlant>>> Get();
    ValueTask<Result<IPlant>> Get(int id);
}