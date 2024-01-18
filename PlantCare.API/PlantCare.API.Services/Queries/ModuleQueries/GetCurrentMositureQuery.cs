using LanguageExt.Common;
using MediatR;
using PlantCare.API.Services.Responses;

namespace PlantCare.API.Services.Queries.ModuleQueries;

public class GetCurrentMositureQuery : IRequest<Result<IReadOnlyCollection<GetCurrentMoistureResponse>>>
{
    public Guid Id { get; set; }
}