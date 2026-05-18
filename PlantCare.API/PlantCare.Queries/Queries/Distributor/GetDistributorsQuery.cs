using LanguageExt.Common;
using MediatR;
using PlantCare.Domain.Dto;

namespace PlantCare.Queries.Queries.Distributor;

public class GetDistributorsQuery : IRequest<Result<IReadOnlyList<Responses.Distributor.Distributor>>>
{
    public int UserId { get; set; }
}