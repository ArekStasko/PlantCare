using LanguageExt.Common;
using PlantCare.Persistance.DAO.Plant;

namespace PlantCare.Persistance.Interfaces.WriteRepositories;

public interface IWritePlantRepository
{
    ValueTask<Result<bool>> Create(IPlantDAO plant);
    ValueTask<Result<bool>> Delete(int id);
    ValueTask<Result<bool>> Update(IPlantDAO plant);
}