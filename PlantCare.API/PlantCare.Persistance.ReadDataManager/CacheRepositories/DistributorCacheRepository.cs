using LanguageExt.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PlantCare.Domain.Models.Distributor;
using PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;

namespace PlantCare.Persistance.ReadDataManager.CacheRepositories;

public class DistributorCacheRepository(
    IReadDistributorRepository readRepository,
    ILogger<ModuleCacheRepository> logger,
    IDistributedCache cache)
    : IReadDistributorRepository
{

    public async Task<Result<IDistributor>> GetDistributor(int id)
    {
        string distributorKey = $"distributor_{id}";
        IDistributor data = await cache.GetRecordAsync<Distributor>(distributorKey);

        if (data == null)
        {
            logger.LogInformation("Saving Distributor to cache");
            var distributor = await readRepository.GetDistributor(id);
            return await distributor.ProcessCacheResult(cache, distributorKey);
        }

        return new Result<IDistributor>(data!);
    }

    public async Task<Result<IReadOnlyCollection<IDistributor>>> GetDistributors(int userId)
    {
        string distributorsKey = $"distributors_{userId}";
        IReadOnlyCollection<IDistributor> data = await cache.GetRecordAsync<List<Distributor>>(distributorsKey);

        if (data == null)
        {
            logger.LogInformation("Saving Distributors to cache");
            var distributors = await readRepository.GetDistributors(userId);
            return await distributors.ProcessCacheResult(cache, distributorsKey);
        }

        return new Result<IReadOnlyCollection<IDistributor>>(data!);
    }
}