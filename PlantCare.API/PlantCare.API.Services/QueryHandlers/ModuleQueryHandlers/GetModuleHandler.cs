using LanguageExt.Common;
using MediatR;
using PlantCare.API.DataAccess.Models.Module;
using PlantCare.API.Services.Queries.ModuleQueries;

namespace PlantCare.API.Services.QueryHandlers.ModuleQueryHandlers;

public class GetModuleHandler : IRequestHandler<GetModuleQuery, Result<IModule>>
{
    public Task<Result<IModule>> Handle(GetModuleQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}