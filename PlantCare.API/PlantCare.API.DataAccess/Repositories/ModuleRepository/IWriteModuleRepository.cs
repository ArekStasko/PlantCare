using LanguageExt.Common;
using PlantCare.API.DataAccess.Models.Module;

namespace PlantCare.API.DataAccess.Repositories.ModuleRepository;

public interface IWriteModuleRepository
{
    ValueTask<Result<bool>> Add(int id);
    ValueTask<Result<bool>> Delete(int id);
    ValueTask<Result<bool>> Update(IModule module);
}