using LanguageExt.Common;
using MediatR;
using PlantCare.Domain.Models.Place;

namespace PlantCare.Queries.Queries.Place;

public record GetPlacesQuery : IRequest<Result<IReadOnlyCollection<IPlace>>>;