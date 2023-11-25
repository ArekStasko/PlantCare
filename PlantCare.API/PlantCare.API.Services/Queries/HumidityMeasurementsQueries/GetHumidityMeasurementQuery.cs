using LanguageExt.Common;
using MediatR;
using PlantCare.API.DataAccess.Models.HumidityMeasurement;

namespace PlantCare.API.Services.Queries.HumidityMeasurementsQueries;

public class GetHumidityMeasurementQuery : IRequest<Result<IReadOnlyCollection<IHumidityMeasurement>>>
{
    public Guid Id { get; set; }
}