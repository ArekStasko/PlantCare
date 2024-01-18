using LanguageExt.Common;
using PlantCare.Domain.Models.Place;

namespace PlantCare.Persistance.Interfaces.ReadRepositories;

public interface IReadPlaceRepository
{
    ValueTask<Result<IReadOnlyCollection<IPlace>>> Get();
}