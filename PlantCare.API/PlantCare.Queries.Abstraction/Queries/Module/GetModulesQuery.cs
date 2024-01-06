using LanguageExt.Common;
using MediatR;
using PlantCare.Queries.Abstraction.Responses.Module;

namespace PlantCare.Queries.Abstraction.Queries.Module;

public record GetModulesQuery : IRequest<Result<IReadOnlyCollection<GetModulesResponse>>>;