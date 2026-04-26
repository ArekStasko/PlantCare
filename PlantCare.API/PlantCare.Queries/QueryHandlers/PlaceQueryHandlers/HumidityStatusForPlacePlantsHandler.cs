using AutoMapper;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Domain.Enums;
using PlantCare.Domain.Models.Plant;
using PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;
using PlantCare.Queries.Queries.Place;
using PlantCare.Queries.Responses.HumidityMeasurements;
using PlantCare.Queries.Responses.Place;

namespace PlantCare.Queries.QueryHandlers.PlaceQueryHandlers;

public class HumidityStatusForPlacePlantsHandler : IRequestHandler<GetHumidityStatusForPlacePlantsQuery, Result<IReadOnlyCollection<PlantHumidityStatus>>>
{
    private readonly IReadPlantRepository _repository;
    private readonly IReadHumidityMeasurementRepository _repositoryHumidityMeasurement;
    private readonly IMapper _mapper;
    private readonly ILogger<PlaceQueryHandler> _logger;

    public HumidityStatusForPlacePlantsHandler(IReadPlantRepository repository, IReadHumidityMeasurementRepository repositoryHumidityMeasurement, IMapper mapper, ILogger<PlaceQueryHandler> logger)
    {
        _repository = repository;
        _repositoryHumidityMeasurement = repositoryHumidityMeasurement;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<IReadOnlyCollection<PlantHumidityStatus>>> Handle(GetHumidityStatusForPlacePlantsQuery query, CancellationToken cancellationToken)
    {
        try
        {

            var plants = (await _repository.Get(query.UserId)).Match<IReadOnlyCollection<IPlant>>(
                succ 
                    => succ.Where(p => p.PlaceId == query.Id).ToList(), 
                err 
                    => {
                        _logger.LogError("Something went wrong while fetching plants: {err}", err);
                        throw err;
                    });
            
            List<Task<Result<(int, int)>>> tasks = new List<Task<Result<(int, int)>>>();
            foreach (var plant in plants)
            {
                tasks.Add(_repositoryHumidityMeasurement.GetLatest(plant.Id));
            }
            
            var statuses = await Task.WhenAll(tasks);
            List<PlantHumidityStatus> result = new List<PlantHumidityStatus>();

            foreach (var status in statuses)
            {
                status.Match(succ =>
                {
                    result.Add(new PlantHumidityStatus()
                    {
                        PlantId = succ.Item1,
                        Status = succ.Item2
                    });
                }, err =>
                {
                    _logger.LogError("Get latest humidity record failed for plant in {Id} place", query.Id);
                });
            }

            return result;
        }
        catch (Exception e)
        {
            _logger.LogError("Exception has been thrown in HumidityStatusForPlacePlantsHandler: {exception}", e.Message);
            return new Result<IReadOnlyCollection<PlantHumidityStatus>>(e);
        }
    }

    private HumidityStatus GetHumidityStatus(int humidity)
    {
        switch (humidity)
        {
            
        }
    }
}