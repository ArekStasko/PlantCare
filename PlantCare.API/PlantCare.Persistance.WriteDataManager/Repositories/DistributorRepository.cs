using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlantCare.Domain.Models.Distributor;
using PlantCare.Persistance.WriteDataManager.Interfaces;
using PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;

namespace PlantCare.Persistance.WriteDataManager.Repositories;

public class DistributorRepository(IDistributorContext context, ILogger<DistributorRepository> logger)
    : IWriteDistributorRepository
{
    public async ValueTask<Result<int>> Create(int userId, string name)
    {
        try
        {
            var distributor = new Distributor()
            {
                Name = name,
                UserId = userId
            };
            await context.Distributors.AddAsync(distributor);
            await context.SaveChangesAsync();
            logger.LogInformation("Distributor with {Id} Id was successfully created", distributor.Id);
            return new Result<int>(distributor.Id);
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return new Result<int>(e);
        }
    }

    public async ValueTask<Result<bool>> WaterSupply(int userId, int distributorId, int plantId)
    {
        try
        {
            var waterSupply = new WaterSupply()
            {
                DistributorId = distributorId,
                PlantId = plantId,
                UserId = userId
            };
            await context.WaterSupplies.AddAsync(waterSupply);
            await context.SaveChangesAsync();
            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            logger.LogError("Something went wrong while running water supply for distributor {distributorId}: {e}", distributorId, e);
            return new Result<bool>(e);
        }
    }
    
    public async ValueTask<Result<bool>> RemoveWaterSupply(int distributorId, int plantId)
    {
        try
        {
            var waterSupplyToDelete = context.WaterSupplies.SingleOrDefault(w => w.DistributorId == distributorId && w.PlantId == plantId);

            if (waterSupplyToDelete == null)
            {
                logger.LogError("There is no water supply for distributor: {distributorId} with plant: {plantId}", distributorId, plantId);
                return new Result<bool>(false);
            }
            
            context.WaterSupplies.Remove(waterSupplyToDelete);
            await context.SaveChangesAsync();
            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            logger.LogError("Something went wrong while running water supply for distributor {distributorId}: {e}", distributorId, e);
            return new Result<bool>(e);
        }
    }
}