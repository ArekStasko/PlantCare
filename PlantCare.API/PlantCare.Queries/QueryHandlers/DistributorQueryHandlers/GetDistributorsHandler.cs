using AutoMapper;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Domain.Dto;
using PlantCare.Persistance.ReadDataManager.Repositories;
using PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;
using PlantCare.Queries.Queries.Distributor;
using PlantCare.Queries.Responses.Distributor;

namespace PlantCare.Queries.QueryHandlers.DistributorQueryHandlers;

public class GetDistributorsHandler(
    IReadDistributorRepository repository,
    IMapper mapper,
    ILogger<GetDistributorsHandler> logger
    ) : IRequestHandler<GetDistributorsQuery, Result<IReadOnlyList<Distributor>>>
{
    public async Task<Result<IReadOnlyList<Distributor>>> Handle(GetDistributorsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var distributors = await repository.GetDistributors(request.UserId);
            return distributors.Match(succ =>
            {
                var result = mapper.Map<IReadOnlyList<Distributor>>(succ);
                return new Result<IReadOnlyList<Distributor>>(result);
            }, err =>
            {
                logger.LogError("Something went wrong while executing get distributors repository method: {err}", err);
                return new Result<IReadOnlyList<Distributor>>(err);
            });
        }
        catch (Exception e)
        {
            logger.LogError("Something went wrong while fetching distributors");
            return new Result<IReadOnlyList<Distributor>>(e);
        }
    }
}