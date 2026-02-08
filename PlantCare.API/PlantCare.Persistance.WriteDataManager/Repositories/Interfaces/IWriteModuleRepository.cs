using LanguageExt.Common;
using PlantCare.Domain.Models.Module;

namespace PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;

public interface IWriteModuleRepository
{
    ValueTask<Result<int>> Add(int userId, string name, string address);
}