using LanguageExt.Common;
using PlantCare.API.DataAccess.Models.Module;

namespace PlantCare.API.DataAccess.Repositories.ModuleRepository;

public interface IReadModuleRepository
{
    ValueTask<Result<IReadOnlyCollection<IModule>>> Get();

    ValueTask<Result<IModule>> Get(Guid id);
}