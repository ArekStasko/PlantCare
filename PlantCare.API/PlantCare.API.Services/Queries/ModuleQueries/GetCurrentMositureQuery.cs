using LanguageExt.Common;
using MediatR;

namespace PlantCare.API.Services.Queries.ModuleQueries;

public class GetCurrentMositureQuery : IRequest<Result<int>>
{
    public int Id { get; set; }
}