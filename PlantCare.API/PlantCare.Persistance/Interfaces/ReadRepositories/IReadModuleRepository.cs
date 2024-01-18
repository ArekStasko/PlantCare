using LanguageExt.Common;
using PlantCare.Domain.Models.Module;

namespace PlantCare.Persistance.Interfaces.ReadRepositories;

public interface IReadModuleRepository
{
    ValueTask<Result<IReadOnlyCollection<IModule>>> Get();

    ValueTask<Result<IModule>> Get(Guid id);
}