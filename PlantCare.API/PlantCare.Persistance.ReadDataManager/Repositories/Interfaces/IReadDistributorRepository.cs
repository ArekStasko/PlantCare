using LanguageExt.Common;
using PlantCare.Domain.Models.Distributor;

namespace PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;

public interface IReadDistributorRepository
{
    public Task<Result<IDistributor>> GetDistributor(int id);
    public Task<Result<IReadOnlyCollection<IDistributor>>> GetDistributors(int userId);
}