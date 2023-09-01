using LanguageExt.Common;
using PlantCare.API.DataAccess.Models;

namespace PlantCare.API.DataAccess.Repositories.PlantRepository;

public interface IPlantRepository
{
    Result<bool> Create(IPlant plant);
    Result<bool> Delete(int id);
    Result<bool> Edit(IPlant plant);
    Result<List<IPlant>> GetAll();
    Result<IPlant> GetById(int id);
}