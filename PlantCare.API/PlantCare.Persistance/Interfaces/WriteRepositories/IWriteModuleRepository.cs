using LanguageExt.Common;
using PlantCare.Persistance.DAO.Module;

namespace PlantCare.Persistance.Interfaces.WriteRepositories;

public interface IWriteModuleRepository
{
    ValueTask<Result<Guid>> Add(Guid id);
    ValueTask<Result<bool>> Delete(Guid id);
    ValueTask<Result<bool>> Update(IModuleDAO module);
}