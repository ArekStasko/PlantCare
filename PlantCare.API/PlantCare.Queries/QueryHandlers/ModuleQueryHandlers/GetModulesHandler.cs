using AutoMapper;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Queries.Abstraction.Queries.Module;
using PlantCare.Queries.Abstraction.Responses.Module;

namespace PlantCare.Queries.QueryHandlers.ModuleQueryHandlers;

public class GetModulesHandler : IRequestHandler<GetModulesQuery, Result<IReadOnlyCollection<GetModulesResponse>>>
{
    private readonly IReadModuleRepository _repository;
    private readonly ILogger<GetModulesHandler> _logger;
    private readonly IMapper _mapper;

    public GetModulesHandler(IReadModuleRepository repository, ILogger<GetModulesHandler> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Result<IReadOnlyCollection<GetModulesResponse>>> Handle(GetModulesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("GetModulesHandler start processing");
            var result = await _repository.Get();
            return result.Match(succ =>
            {
                _logger.LogInformation("Successfully processed GetModulesHandler query handler");
                IReadOnlyCollection<GetModulesResponse> result = succ.Select(x => _mapper.Map<GetModulesResponse>(x))
                    .ToList();
                return new Result<IReadOnlyCollection<GetModulesResponse>>(result);
            }, err =>
            {
                _logger.LogError("Something went wrong while processing GetModulesHandler request");
                return new Result<IReadOnlyCollection<GetModulesResponse>>(err);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Exception has been thrown in GetModulesHandler: {exception}", e.Message);
            return new Result<IReadOnlyCollection<GetModulesResponse>>(e);
        }
    }
}