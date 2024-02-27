using LanguageExt.Common;
using PlantCare.Domain.Models.Module;

namespace PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;

public interface IWriteModuleRepository
{
    ValueTask<Result<Guid>> Add(Guid id);
    ValueTask<Result<bool>> Delete(Guid id);
    ValueTask<Result<bool>> Update(IModule module);
}