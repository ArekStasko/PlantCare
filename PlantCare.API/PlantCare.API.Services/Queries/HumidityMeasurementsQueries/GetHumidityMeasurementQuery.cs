using System.Runtime.InteropServices.JavaScript;
using LanguageExt.Common;
using MediatR;
using PlantCare.API.DataAccess.Models.HumidityMeasurement;

namespace PlantCare.API.Services.Queries.HumidityMeasurementsQueries;

public class GetHumidityMeasurementQuery : IRequest<Result<IReadOnlyCollection<IHumidityMeasurement>>>
{
    public Guid Id { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }
}