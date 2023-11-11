using LanguageExt.Common;
using PlantCare.API.DataAccess.Models.Module;

namespace PlantCare.API.DataAccess.Repositories.ModuleRepository;

public class ModuleRepository : IModuleRepository
{
    public ValueTask<Result<bool>> Create(IModule module)
    {
        throw new NotImplementedException();
    }

    public ValueTask<Result<bool>> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public ValueTask<Result<bool>> Update(IModule module)
    {
        throw new NotImplementedException();
    }

    public ValueTask<Result<IReadOnlyCollection<IModule>>> Get()
    {
        throw new NotImplementedException();
    }

    public ValueTask<Result<IModule>> Get(int id)
    {
        throw new NotImplementedException();
    }
}