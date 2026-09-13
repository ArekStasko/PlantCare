using LanguageExt.Common;
using MediatR;
using PlantCare.Queries.Responses.Distributor;

namespace PlantCare.Queries.Queries.WaterSupply;

public class GetWaterSupplyByDistributorId : IRequest<Result<WaterSupplyWithPlant>>
{
    public int DistributorId { get; set; }
}