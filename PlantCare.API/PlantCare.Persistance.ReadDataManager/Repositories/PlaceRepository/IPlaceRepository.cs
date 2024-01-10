using LanguageExt.Common;
using PlantCare.Persistance.DAO.Place;

namespace PlantCare.Persistance.ReadDataManager.Repositories.PlaceRepository;

public interface IPlaceRepository
{
    ValueTask<Result<IReadOnlyCollection<IPlaceDAO>>> Get();
}