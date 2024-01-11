using LanguageExt.Common;
using PlantCare.Persistance.DAO.Module;

namespace PlantCare.Persistance.WriteDataManager.Repositories.ModuleRepository;

public interface IModuleRepository
{
    ValueTask<Result<Guid>> Add(Guid id);
    ValueTask<Result<bool>> Delete(Guid id);
    ValueTask<Result<bool>> Update(IModuleDAO module);
}