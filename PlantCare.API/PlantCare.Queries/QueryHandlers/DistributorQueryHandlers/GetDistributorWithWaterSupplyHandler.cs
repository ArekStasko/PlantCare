using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;
using PlantCare.Queries.Queries.Distributor;
using PlantCare.Queries.Responses.Distributor;

namespace PlantCare.Queries.QueryHandlers.DistributorQueryHandlers;

public class GetDistributorWithWaterSupplyHandler(
    IReadDistributorRepository repository,
    ILogger<GetWaterSupplyByDistributorIdHandler> logger
    ) : IRequestHandler<GetDistributorWithWaterSupplyDataQuery, Result<DistributorWithWaterSupply>>
{
    public async Task<Result<DistributorWithWaterSupply>> Handle(GetDistributorWithWaterSupplyDataQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var isWaterSupplyActive = 
                (await repository.GetWaterSupplyByDistributorIdAndPlantId(request.Id, request.PlantId)).Match(succ => succ,
                    err => throw err);
            var distributor = 
                (await repository.GetDistributor(request.Id, request.UserId)).Match(succ => succ, 
                    err => throw err);

            var distributorWithWaterSupply = new DistributorWithWaterSupply
            {
                Id = distributor.Id,
                Name = distributor.Name,
                IsWaterSupplyActive = isWaterSupplyActive != null
            };
            
            return new Result<DistributorWithWaterSupply>(distributorWithWaterSupply);
        }
        catch (Exception e)
        {
            logger.LogError("Something went wrong in get distributor with water supply handler: {e}", e);
            return new Result<DistributorWithWaterSupply>(e);
        }
    }
}