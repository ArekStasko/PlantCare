using LanguageExt.Common;
using MediatR;
using PlantCare.Queries.Abstraction.Responses.HumidityMeasurements;

namespace PlantCare.Queries.Abstraction.Queries.HumidityMeasurements;

public record GetHumidityMeasurementQuery : IRequest<Result<List<GetHumidityMeasurementsResponse>>>
{
    public Guid Id { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }
}