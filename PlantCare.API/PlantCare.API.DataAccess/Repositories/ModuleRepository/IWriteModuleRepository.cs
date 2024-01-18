using LanguageExt.Common;
using PlantCare.API.DataAccess.Models.Module;

namespace PlantCare.API.DataAccess.Repositories.ModuleRepository;

public interface IWriteModuleRepository
{
    ValueTask<Result<Guid>> Add(Guid id);
    ValueTask<Result<bool>> Delete(Guid id);
    ValueTask<Result<bool>> Update(IModule module);
}