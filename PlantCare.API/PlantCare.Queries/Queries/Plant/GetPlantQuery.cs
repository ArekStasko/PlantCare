using LanguageExt.Common;
using MediatR;
using PlantCare.Domain.Models.Plant;
using PlantCare.Queries.Responses.Plants;

namespace PlantCare.Queries.Queries.Plant;

public record GetPlantQuery : IRequest<Result<Responses.Plants.Plant>>
{
    public int Id { get; set; }
    public int UserId { get; set; }
}