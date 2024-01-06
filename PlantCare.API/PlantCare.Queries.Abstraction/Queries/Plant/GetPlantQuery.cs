using LanguageExt.Common;
using MediatR;

namespace PlantCare.Queries.Abstraction.Queries.Plant;

public record GetPlantQuery : IRequest<Result<IPlant>>
{
    public int Id { get; set; }
}