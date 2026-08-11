using AutoMapper;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;
using PlantCare.Queries.Queries.Distributor;
using PlantCare.Queries.Responses.Distributor;

namespace PlantCare.Queries.QueryHandlers.DistributorQueryHandlers;

public class GetDistributorByIdHandler(
    IReadDistributorRepository repository,
    IMapper mapper,
    ILogger<GetDistributorByIdHandler> logger
    ) : IRequestHandler<GetDistributorByIdQuery, Result<Distributor?>>
{

    public async Task<Result<Distributor?>> Handle(GetDistributorByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var distributor = await repository.GetDistributor(request.Id, request.UserId);
            return distributor.Match(succ =>
            {
                if (succ is null)
                {
                    return new Result<Distributor?>();
                }
                var result = mapper.Map<Distributor?>(succ);
                return new Result<Distributor?>(result);
            }, err =>
            {
                logger.LogError($"Error occured while getting distributor {request.Id}");
                return new Result<Distributor?>(err);
            });
        }
        catch (Exception e)
        {
            logger.LogError($"Error occured while getting distributor {request.Id}");
            return new Result<Distributor>(e);
        }
    }
}