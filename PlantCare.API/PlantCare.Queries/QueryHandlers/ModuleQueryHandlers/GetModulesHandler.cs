using AutoMapper;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;
using PlantCare.Queries.Queries.Module;
using PlantCare.Queries.Responses.Module;

namespace PlantCare.Queries.QueryHandlers.ModuleQueryHandlers;

public class GetModulesHandler : IRequestHandler<GetModulesQuery, Result<IReadOnlyCollection<Module>>>
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

    public async Task<Result<IReadOnlyCollection<Module>>> Handle(GetModulesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _repository.Get(request.UserId);
            return result.Match(succ =>
            {
                IReadOnlyCollection<Module> result = succ.Select(x => _mapper.Map<Module>(x))
                    .ToList();
                return new Result<IReadOnlyCollection<Module>>(result);
            }, err =>
            {
                return new Result<IReadOnlyCollection<Module>>(err);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Exception has been thrown in GetModulesHandler: {exception}", e.Message);
            return new Result<IReadOnlyCollection<Module>>(e);
        }
    }
}