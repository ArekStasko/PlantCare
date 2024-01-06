using LanguageExt.Common;
using MediatR;
using PlantCare.Queries.Abstraction.Responses.Module;

namespace PlantCare.Queries.Abstraction.Queries.Module;

public record GetCurrentMoistureQuery : IRequest<Result<IReadOnlyCollection<GetCurrentMoistureResponse>>>
{
    public Guid Id { get; set; }
}