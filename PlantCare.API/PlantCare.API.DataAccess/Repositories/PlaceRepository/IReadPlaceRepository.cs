using LanguageExt.Common;
using PlantCare.API.DataAccess.Models.Place;

namespace PlantCare.API.DataAccess.Repositories.PlaceRepository;

public interface IReadPlaceRepository
{
    ValueTask<Result<IReadOnlyCollection<IPlace>>> Get();
}