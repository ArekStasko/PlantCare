using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PlantCare.Domain.Models.Distributor;
using PlantCare.Persistance.ReadDataManager.Interfaces;
using PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;

namespace PlantCare.Persistance.ReadDataManager.Repositories;

public class DistributorRepository(
    IDistributorReadContext context,
    ILogger<DistributorRepository> logger,
    IDistributedCache cache)
    : IReadDistributorRepository
{

    public async Task<Result<IDistributor>> GetDistributor(int id)
    {
        try
        {
            var distributor = await context.Distributors.SingleOrDefaultAsync(d => d.Id == id);
            if (distributor == null)
            {
                logger.LogError("There is no distributor with id : {id}", id);
                return new Result<IDistributor>(new NullReferenceException());
            }
            return new Result<IDistributor>(distributor);
        }
        catch (Exception e)
        {
            logger.LogError("Something went wrong while fetching distributor with id: {id}", id);
            return new Result<IDistributor>(e);
        }
    }

    public async Task<Result<IReadOnlyCollection<IDistributor>>> GetDistributors(int userId)
    {
        try
        {
            var distributors = await context.Distributors.Where(d => d.UserId == userId).ToListAsync();
            return new Result<IReadOnlyCollection<IDistributor>>(distributors);
        }
        catch (Exception e)
        {
            logger.LogError("Something went wrong while fetching distributors");
            return new Result<IReadOnlyCollection<IDistributor>>(e);
        }
    }
}