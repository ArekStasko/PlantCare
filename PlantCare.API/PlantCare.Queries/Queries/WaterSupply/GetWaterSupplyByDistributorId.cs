using LanguageExt.Common;
using MediatR;

namespace PlantCare.Queries.Queries.WaterSupply;

public class GetWaterSupplyByDistributorId : IRequest<Result<int>>
{
    public int UserId { get; set; }
    public int DistributorId { get; set; }
}