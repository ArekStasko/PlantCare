using LanguageExt.Common;
using MediatR;
using PlantCare.Queries.Responses.HumidityMeasurements;

namespace PlantCare.Queries.Queries.Place;

public class GetHumidityStatusForPlacePlants : IRequest<Result<IReadOnlyCollection<PlantHumidityStatus>>>
{
    public int UserId { get; set; }
    public int Id { get; set; }
}