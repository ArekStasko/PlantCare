using LanguageExt.Common;
using PlantCare.API.DataAccess.Models.Module;
using PlantCare.API.DataAccess.Repositories.ModuleRepository;

namespace PlantCare.API.Services.Tests.Utils;

public class MockModuleRepo : IReadModuleRepository, IWriteModuleRepository
{
    public virtual ValueTask<Result<IReadOnlyCollection<IModule>>> Get()
    {
        throw new NotImplementedException();
    }

    public virtual ValueTask<Result<IModule>> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public virtual ValueTask<Result<Guid>> Add(Guid id)
    {
        throw new NotImplementedException();
    }

    public virtual ValueTask<Result<bool>> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public virtual ValueTask<Result<bool>> Update(IModule module)
    {
        throw new NotImplementedException();
    }
}