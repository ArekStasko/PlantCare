using LanguageExt.Common;
using PlantCare.Persistance.DAO.Plant;

namespace PlantCare.Persistance.Interfaces.ReadRepositories;

public interface IReadPlantRepository
{
    ValueTask<Result<IReadOnlyCollection<IPlantDAO>>> Get();
    ValueTask<Result<IPlantDAO>> Get(int id);
}