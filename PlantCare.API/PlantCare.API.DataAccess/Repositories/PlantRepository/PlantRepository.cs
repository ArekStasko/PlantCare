using LanguageExt.Common;
using PlantCare.API.DataAccess.Models;

namespace PlantCare.API.DataAccess.Repositories.PlantRepository;

public class PlantRepository : IPlantRepository
{
    public Result<bool> Create(IPlant plant)
    {
        throw new NotImplementedException();
    }

    public Result<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Result<bool> Edit(IPlant plant)
    {
        throw new NotImplementedException();
    }

    public Result<List<IPlant>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Result<IPlant> GetById(int id)
    {
        throw new NotImplementedException();
    }
}