using LanguageExt.Common;
using PlantCare.Domain.Models.Module;

namespace PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;

public interface IWriteModuleRepository
{
    ValueTask<Result<Guid>> Add(int userId, Guid id);
    ValueTask<Result<bool>> Delete(int userId, Guid id);
    ValueTask<Result<bool>> Update(IModule module);
}