using LanguageExt.Common;
using PlantCare.Domain.Models.Module;

namespace PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;

public interface IWriteModuleRepository
{
    ValueTask<Result<int>> Add(int userId);
    ValueTask<Result<bool>> Delete(int userId, int id);
    ValueTask<Result<bool>> Update(IModule module);
}