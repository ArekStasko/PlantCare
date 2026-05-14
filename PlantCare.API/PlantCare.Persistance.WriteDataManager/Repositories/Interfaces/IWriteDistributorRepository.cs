using LanguageExt.Common;

namespace PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;

public interface IWriteDistributorRepository
{
    ValueTask<Result<int>> Add(int userId, string name);
}