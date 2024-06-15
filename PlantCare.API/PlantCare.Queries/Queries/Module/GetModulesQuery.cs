using LanguageExt.Common;
using MediatR;
using PlantCare.Queries.Responses.Module;

namespace PlantCare.Queries.Queries.Module;

public record GetModulesQuery : IRequest<Result<IReadOnlyCollection<GetModulesResponse>>>
{
    public int UserId { get; set; }
}