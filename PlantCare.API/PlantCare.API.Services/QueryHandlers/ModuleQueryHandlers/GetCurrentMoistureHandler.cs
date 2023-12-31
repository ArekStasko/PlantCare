using AutoMapper;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Repositories.HumidityMeasurementRepository;
using PlantCare.API.Services.Queries.ModuleQueries;
using PlantCare.API.Services.Responses;

namespace PlantCare.API.Services.QueryHandlers.ModuleQueryHandlers;

public class GetCurrentMoistureHandler : IRequestHandler<GetCurrentMositureQuery, Result<IReadOnlyCollection<GetCurrentMoistureResponse>>>
{
    private readonly IReadHumidityMeasurementRepository _repository;
    private readonly ILogger<GetCurrentMoistureHandler> _logger;
    private readonly IMapper _mapper;
    
    public GetCurrentMoistureHandler(IReadHumidityMeasurementRepository repository, ILogger<GetCurrentMoistureHandler> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Result<IReadOnlyCollection<GetCurrentMoistureResponse>>> Handle(GetCurrentMositureQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("GetCurrentMoistureHandler start processing");
            var result = await _repository.Get(request.Id);
            return result.Match(succ =>
            {
                IReadOnlyCollection<GetCurrentMoistureResponse> result = succ.Select(x => _mapper.Map<GetCurrentMoistureResponse>(x))
                    .ToList();
                _logger.LogInformation("GetCurrentMoistureHandler successfully loaded current moisture level");
                return new Result<IReadOnlyCollection<GetCurrentMoistureResponse>>(result);
            }, err =>
            {
                _logger.LogError("Something went wrong while processing GetCurrentMoistureHandler request");
                return new Result<IReadOnlyCollection<GetCurrentMoistureResponse>>(err);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Exception has been thrown in GetModulesHandler: {exception}", e.Message);
            return new Result<IReadOnlyCollection<GetCurrentMoistureResponse>>(e);
        }
    }
}