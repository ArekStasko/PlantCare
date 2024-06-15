using LanguageExt.Common;
using MediatR;
using PlantCare.Domain.Models.Plant;
using PlantCare.Queries.Responses.Plants;

namespace PlantCare.Queries.Queries.Plant;

public record GetPlantsQuery : IRequest<Result<IReadOnlyCollection<GetPlantResponse>>>
{
    public int UserId { get; set; }
}