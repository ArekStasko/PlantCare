using LanguageExt.Common;
using MediatR;
using PlantCare.Queries.Responses.HumidityMeasurements;

namespace PlantCare.Queries.Queries.HumidityMeasurements;

public record GetHumidityMeasurementQuery : IRequest<Result<List<GetHumidityMeasurementsResponse>>>
{
    public Guid Id { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }
}