using LanguageExt.Common;
using PlantCare.Domain.Models.Module;

namespace PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;

public interface IReadModuleRepository
{
    ValueTask<Result<IReadOnlyCollection<IModule>>> Get();
    ValueTask<Result<IReadOnlyCollection<IModule>>> Get(int userId);

    ValueTask<Result<IModule>> Get(int userId, int id);
}