using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;
using PlantCare.Queries.Queries.WaterSupply;
using PlantCare.Queries.Responses.Distributor;

namespace PlantCare.Queries.QueryHandlers.DistributorQueryHandlers;

public class GetWaterSupplyByDistributorIdHandler(
        IReadDistributorRepository repository,
        ILogger<GetWaterSupplyByDistributorIdHandler> logger
    ) : IRequestHandler<GetWaterSupplyByDistributorId, Result<WaterSupplyWithPlant>>
{
    public async Task<Result<WaterSupplyWithPlant>> Handle(GetWaterSupplyByDistributorId request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await repository.GetFirstWaterSupplyByDistributorId(request.DistributorId);
            var waterSupplyWithPlant = new WaterSupplyWithPlant()
            {
                Id = -1,
                PlantId = -1
            };
            return result.Match(succ =>
            {
                if (succ != null)
                {
                    waterSupplyWithPlant.Id = succ.Id;
                    waterSupplyWithPlant.PlantId = succ.PlantId;
                }
                return new Result<WaterSupplyWithPlant>(waterSupplyWithPlant);
            }, err =>
            {
                logger.LogError(
                    "Something went wrong while fetching water supply record from distributor repository: {e}", err);
                return new Result<WaterSupplyWithPlant>(err);
            });
        }
        catch (Exception e)
        {
            logger.LogError("Something went wrong while handling water supply status query: {e}", e);
            return new Result<WaterSupplyWithPlant>(e);
        }
    }
}