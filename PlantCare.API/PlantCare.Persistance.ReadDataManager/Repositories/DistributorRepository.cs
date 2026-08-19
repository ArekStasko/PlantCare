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

    public async Task<Result<IDistributor?>> GetDistributor(int id, int userId)
    {
        try
        {
            var distributor = await context.Distributors.SingleOrDefaultAsync(d => d.Id == id && d.UserId == userId);
            if (distributor == null)
            {
                logger.LogError("There is no distributor with id : {id}", id);
                return new Result<IDistributor?>();
            }
            return new Result<IDistributor?>(distributor);
        }
        catch (Exception e)
        {
            logger.LogError("Something went wrong while fetching distributor with id: {id}; Exception: {e}", id, e);
            return new Result<IDistributor?>(e);
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
            logger.LogError("Something went wrong while fetching distributors from database: {e}", e);
            return new Result<IReadOnlyCollection<IDistributor>>(e);
        }
    }

    public async Task<Result<WaterSupply?>> GetFirstWaterSupplyByDistributorId(int id)
    {
        try
        {
            var waterSupplies = await context.WaterSupplies.Where(d => d.DistributorId == id).ToListAsync();
            return new Result<WaterSupply?>(waterSupplies.FirstOrDefault());
        }
        catch (Exception e)
        {
            logger.LogError("Something went wrong while fetching water supplies from database: {e}", e);
            return new Result<WaterSupply?>(e);
        }
    }
}