using LanguageExt.Common;
using MediatR;
using PlantCare.Domain.Models.Plant;

namespace PlantCare.Queries.Abstraction.Queries.Plant;

public record GetPlantQuery : IRequest<Result<IPlant>>
{
    public int Id { get; set; }
}