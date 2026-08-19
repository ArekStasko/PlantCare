using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;
using PlantCare.Queries.Queries.WaterSupply;

namespace PlantCare.Queries.QueryHandlers.DistributorQueryHandlers;

public class GetWaterSupplyByDistributorIdHandler(
        IReadDistributorRepository repository,
        ILogger<GetWaterSupplyByDistributorIdHandler> logger
    ) : IRequestHandler<GetWaterSupplyByDistributorId, Result<int>>
{
    public async Task<Result<int>> Handle(GetWaterSupplyByDistributorId request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await repository.GetFirstWaterSupplyByDistributorId(request.DistributorId);
            return result.Match(succ =>
            {
                if (succ != null) return succ.PlantId;
                return -1;
            }, err =>
            {
                logger.LogError(
                    "Something went wrong while fetching water supply record from distributor repository: {e}", err);
                return new Result<int>(err);
            });
        }
        catch (Exception e)
        {
            logger.LogError("Something went wrong while handling water supply status query: {e}", e);
            return new Result<int>(e);
        }
    }
}