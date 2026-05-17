using AutoMapper;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;
using PlantCare.Queries.Queries.Module;
using PlantCare.Queries.Responses.Module;

namespace PlantCare.Queries.QueryHandlers.ModuleQueryHandlers;

public class GetModuleByIdHandler(IReadModuleRepository repository, ILogger<GetModulesHandler> logger, IMapper mapper)
    : IRequestHandler<GetModuleByIdQuery, Result<Module>>
{
    public async Task<Result<Module>> Handle(GetModuleByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await repository.Get(request.UserId);
            return result.Match(succ =>
            {
                IReadOnlyCollection<Module> result = succ.Select(x => mapper.Map<Module>(x))
                    .ToList();

                var module = result.FirstOrDefault(m => m.Id == request.ModuleId);

                if (module is null)
                {
                    logger.LogError($"Module with id: {request.ModuleId} not found");
                    return new Result<Module>(new ResultIsNullException("Module not found"));
                }
                
                return new Result<Module>(module);
            }, err => 
                new Result<Module>(err));
        }
        catch (Exception e)
        {
            logger.LogError("Exception has been thrown in GetModulesHandler: {exception}", e.Message);
            return new Result<Module>(e);
        }
    }
}