using LanguageExt.Common;
using MediatR;

namespace PlantCare.Queries.Queries.Distributor;

public class GetDistributorByIdQuery : IRequest<Result<Responses.Distributor.Distributor?>>
{
    public int UserId { get; set; }
    public int Id { get; set; }
}