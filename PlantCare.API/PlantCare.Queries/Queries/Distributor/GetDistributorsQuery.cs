using LanguageExt.Common;
using MediatR;

namespace PlantCare.Queries.Queries.Distributor;

public class GetDistributorsQuery : IRequest<Task<Result<IReadOnlyList<Responses.Distributor.Distributor>>>>
{
    public int UserId { get; set; }
}