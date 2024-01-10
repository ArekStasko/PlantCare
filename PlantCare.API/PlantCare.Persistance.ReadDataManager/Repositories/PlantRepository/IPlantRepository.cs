using LanguageExt.Common;
using PlantCare.Persistance.DAO.Plant;

namespace PlantCare.Persistance.ReadDataManager.Repositories.PlantRepository;

public interface IPlantRepository
{
    ValueTask<Result<IReadOnlyCollection<IPlantDAO>>> Get();
    ValueTask<Result<IPlantDAO>> Get(int id);
}