using LanguageExt.Common;
using PlantCare.Domain.Models.Module;

namespace PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;

public interface IWriteModuleRepository
{
    ValueTask<Result<int>> Add(int userId, string name);
    ValueTask<Result<bool>> Delete(int userId, int id);
    ValueTask<Result<IModule>> UpdateStatus(int userId, int moduleId, bool status);
}