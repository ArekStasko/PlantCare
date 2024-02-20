using LanguageExt.Common;
using PlantCare.Domain.Models.Place;

namespace PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;

public interface IWritePlaceRepository
{
    ValueTask<Result<bool>> Create(IPlace place);
    ValueTask<Result<bool>> Delete(int id);
    ValueTask<Result<bool>> Update(IPlace place);
}