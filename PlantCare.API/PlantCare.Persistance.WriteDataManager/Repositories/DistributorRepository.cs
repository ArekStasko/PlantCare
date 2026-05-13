using LanguageExt.Common;
using Microsoft.Extensions.Logging;
using PlantCare.Domain.Models.Distributor;
using PlantCare.Persistance.WriteDataManager.Interfaces;
using PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;

namespace PlantCare.Persistance.WriteDataManager.Repositories;

public class DistributorRepository(IDistributorContext context, ILogger<DistributorRepository> logger)
    : IWriteDistributorRepository
{
    public async ValueTask<Result<int>> Add(int userId, string name)
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

    public ValueTask<Result<bool>> AddPlant(int userId, int distributorId, int plantId)
    {
        try
        {
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public ValueTask<Result<bool>> RemovePlant(int userId, int distributorId, int plantId)
    {
        try
        {
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}