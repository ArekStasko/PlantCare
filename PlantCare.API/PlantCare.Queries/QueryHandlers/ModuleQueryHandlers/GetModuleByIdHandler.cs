using AutoMapper;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;
using PlantCare.Queries.Queries.Module;
using PlantCare.Queries.Responses.Module;

namespace PlantCare.Queries.QueryHandlers.ModuleQueryHandlers;

public class GetModuleByIdHandler : IRequestHandler<GetModuleByIdQuery, Result<Module>>
{
    private readonly IReadModuleRepository _repository;
    private readonly ILogger<GetModulesHandler> _logger;
    private readonly IMapper _mapper;

    public GetModuleByIdHandler(IReadModuleRepository repository, ILogger<GetModulesHandler> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }
    
    public async Task<Result<Module>> Handle(GetModuleByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _repository.Get(request.UserId);
            return result.Match(succ =>
            {
                IReadOnlyCollection<Module> result = succ.Select(x => _mapper.Map<Module>(x))
                    .ToList();

                var module = result.FirstOrDefault(m => m.Id == request.ModuleId);

                if (module is null)
                {
                    _logger.LogError($"Module with id: {request.ModuleId} not found");
                    return new Result<Module>(new ResultIsNullException("Module not found"));
                }
                
                return new Result<Module>(module);
            }, err => 
                new Result<Module>(err));
        }
        catch (Exception e)
        {
            _logger.LogError("Exception has been thrown in GetModulesHandler: {exception}", e.Message);
            return new Result<Module>(e);
        }
    }
}