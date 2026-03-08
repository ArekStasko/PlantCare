using LanguageExt.Common;
using MediatR;
using PlantCare.Domain.Dto;

namespace PlantCare.Queries.Queries.HumidityMeasurements;

public class GetAverageHumidityMeasurementQuery : IRequest<Result<List<AverageHumidity>>>
{
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public int ModuleId { get; set; }
}