using LanguageExt.Common;
using PlantCare.Domain.Models.Place;

namespace PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;

public interface IReadPlaceRepository
{
    ValueTask<Result<IReadOnlyCollection<IPlace>>> Get(int userId);
}