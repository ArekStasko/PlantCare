using LanguageExt.Common;
using MediatR;
using PlantCare.Queries.Responses.Module;

namespace PlantCare.Queries.Queries.Module;

public class GetModuleByIdQuery : IRequest<Result<Responses.Module.Module>>
{
    public int UserId { get; set; }
    public int ModuleId { get; set; }
}