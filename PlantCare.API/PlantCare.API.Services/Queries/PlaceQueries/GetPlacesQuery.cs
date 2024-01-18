namespace PlantCare.API.Services.Queries.PlaceQueries;

using LanguageExt.Common;
using MediatR;
using PlantCare.API.DataAccess.Models.Place;

public record GetPlacesQuery : IRequest<Result<IReadOnlyCollection<IPlace>>>;