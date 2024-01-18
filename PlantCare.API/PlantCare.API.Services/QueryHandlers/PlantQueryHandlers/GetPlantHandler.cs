using AutoMapper;
using LanguageExt.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Models;
using PlantCare.API.DataAccess.Repositories.PlantRepository;
using PlantCare.API.Services.Requests;

namespace PlantCare.API.Services.Handlers;

public class GetPlantHandler : IRequestHandler<GetPlantQuery, Result<IPlant>>
{
    private readonly IReadPlantRepository _repository;
    private readonly ILogger<GetPlantHandler> _logger;

    public GetPlantHandler(IReadPlantRepository repository, ILogger<GetPlantHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<IPlant>> Handle(GetPlantQuery query, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("GetPlantHandler handles request");
            var plant = await _repository.Get(query.Id);
            return plant.Match(succ =>
            {
                _logger.LogInformation("GetPlantHandler successfully processed the request");
                return new Result<IPlant>(succ);
            }, err =>
            {
                _logger.LogError("Something went wrong while processing GetPlantHandler request");
                return new Result<IPlant>(err);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Exception has been thrown in GetPlantHandler: {exception}", e.Message);
            return new Result<IPlant>(e);
        }
    }
}