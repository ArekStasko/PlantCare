using LanguageExt.Common;
using MediatR;

namespace PlantCare.Queries.Abstraction.Queries.Place;

public record GetPlacesQuery : IRequest<Result<IReadOnlyCollection<IPlace>>>;