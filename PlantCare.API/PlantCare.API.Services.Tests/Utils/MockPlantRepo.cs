using LanguageExt.Common;
using PlantCare.API.DataAccess.Models;
using PlantCare.API.DataAccess.Repositories.PlantRepository;

namespace PlantCare.API.Services.Tests.Utils;

public class MockPlantRepo : IReadPlantRepository, IWritePlantRepository
{
    public virtual ValueTask<Result<IReadOnlyCollection<IPlant>>> Get()
    {
        throw new NotImplementedException();
    }

    public virtual ValueTask<Result<IPlant>> Get(int id)
    {
        throw new NotImplementedException();
    }

    public virtual ValueTask<Result<bool>> Create(IPlant plant)
    {
        throw new NotImplementedException();
    }

    public virtual ValueTask<Result<bool>> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public virtual ValueTask<Result<bool>> Update(IPlant plant)
    {
        throw new NotImplementedException();
    }
}