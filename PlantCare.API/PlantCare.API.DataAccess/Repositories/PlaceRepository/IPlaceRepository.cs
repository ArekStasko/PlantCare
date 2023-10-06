using LanguageExt.Common;
using PlantCare.API.DataAccess.Models;
using PlantCare.API.DataAccess.Models.Place;

namespace PlantCare.API.DataAccess.Repositories.PlaceRepository;

public interface IPlaceRepository
{
    ValueTask<Result<bool>> Create(IPlace plant);
    ValueTask<Result<bool>> Delete(int id);
    ValueTask<Result<bool>> Edit(IPlace plant);
    ValueTask<Result<List<IPlace>>> Get();
}