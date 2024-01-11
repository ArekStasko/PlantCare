using LanguageExt.Common;
using PlantCare.Persistance.DAO.Place;

namespace PlantCare.Persistance.WriteDataManager.Repositories.PlaceRepository;

public interface IPlaceRepository
{
    ValueTask<Result<bool>> Create(IPlaceDAO place);
    ValueTask<Result<bool>> Delete(int id);
    ValueTask<Result<bool>> Update(IPlaceDAO place);
}