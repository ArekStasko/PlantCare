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
}