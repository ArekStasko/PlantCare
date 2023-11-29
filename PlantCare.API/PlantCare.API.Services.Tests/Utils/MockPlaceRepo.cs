using LanguageExt.Common;
using PlantCare.API.DataAccess.Models.Place;
using PlantCare.API.DataAccess.Repositories.PlaceRepository;

namespace PlantCare.API.Services.Tests.Utils;

public class MockPlaceRepo : IReadPlaceRepository, IWritePlaceRepository
{
    public  virtual ValueTask<Result<IReadOnlyCollection<IPlace>>> Get()
    {
        throw new NotImplementedException();
    }

    public virtual ValueTask<Result<bool>> Create(IPlace place)
    {
        throw new NotImplementedException();
    }

    public virtual ValueTask<Result<bool>> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public virtual ValueTask<Result<bool>> Update(IPlace place)
    {
        throw new NotImplementedException();
    }
}