using LanguageExt.Common;
using MediatR;

namespace PlantCare.Queries.Queries.Distributor;

public class GetDistributorWithWaterSupplyDataQuery : IRequest<Result<Responses.Distributor.DistributorWithWaterSupply?>>
{
    public int UserId { get; set; }
    public int Id { get; set; }
    public int PlantId { get; set; }
}