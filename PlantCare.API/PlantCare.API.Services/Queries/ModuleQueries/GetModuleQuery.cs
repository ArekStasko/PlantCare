using LanguageExt.Common;
using MediatR;
using PlantCare.API.DataAccess.Models.Module;

namespace PlantCare.API.Services.Queries.ModuleQueries;

public class GetModuleQuery : IRequest<Result<IModule>>
{
    public int Id { get; set; }
}