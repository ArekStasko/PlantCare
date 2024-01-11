using LanguageExt.Common;
using PlantCare.Persistance.DAO.Module;

namespace PlantCare.Persistance.ReadDataManager.Repositories.ModelRepository;

public interface IModuleRepository
{
    ValueTask<Result<IReadOnlyCollection<IModuleDAO>>> Get();

    ValueTask<Result<IModuleDAO>> Get(Guid id);
}