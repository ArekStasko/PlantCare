using LanguageExt.Common;
using PlantCare.Persistance.DAO.Module;

namespace PlantCare.Persistance.Interfaces.ReadRepositories;

public interface IReadModuleRepository
{
    ValueTask<Result<IReadOnlyCollection<IModuleDAO>>> Get();

    ValueTask<Result<IModuleDAO>> Get(Guid id);
}