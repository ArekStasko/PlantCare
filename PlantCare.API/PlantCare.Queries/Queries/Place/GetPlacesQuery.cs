using LanguageExt.Common;
using MediatR;
using PlantCare.Domain.Models.Place;
using PlantCare.Queries.Responses.Place;

namespace PlantCare.Queries.Queries.Place;

public record GetPlacesQuery : IRequest<Result<IReadOnlyCollection<Responses.Place.Place>>>
{
    public int UserId { get; set; }
}