using LanguageExt.Common;
using PlantCare.API.DataAccess.Models.Place;

namespace PlantCare.API.DataAccess.Repositories.PlaceRepository;

public interface IWritePlaceRepository
{
    ValueTask<Result<bool>> Create(IPlace place);
    ValueTask<Result<bool>> Delete(int id);
    ValueTask<Result<bool>> Update(IPlace place);
}