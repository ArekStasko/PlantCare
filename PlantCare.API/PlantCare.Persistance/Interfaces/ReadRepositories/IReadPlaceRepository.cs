using LanguageExt.Common;
using PlantCare.Persistance.DAO.Place;

namespace PlantCare.Persistance.Interfaces.ReadRepositories;

public interface IReadPlaceRepository
{
    ValueTask<Result<IReadOnlyCollection<IPlaceDAO>>> Get();
}