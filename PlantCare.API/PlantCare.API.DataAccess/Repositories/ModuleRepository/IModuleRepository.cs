using LanguageExt.Common;
using PlantCare.API.DataAccess.Models.Module;
using PlantCare.API.DataAccess.Models.Place;

namespace PlantCare.API.DataAccess.Repositories.ModuleRepository;

public interface IModuleRepository
{
    ValueTask<Result<bool>> Add(int Id);
    ValueTask<Result<bool>> Delete(int id);
    ValueTask<Result<bool>> Update(IModule module);
    ValueTask<Result<IReadOnlyCollection<IModule>>> Get();
    ValueTask<Result<IModule>> Get(int id);
}