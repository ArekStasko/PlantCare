using LanguageExt.Common;
using MediatR;
using PlantCare.Domain.Models.Plant;

namespace PlantCare.Queries.Abstraction.Queries.Plant;

public record GetPlantsQuery : IRequest<Result<IReadOnlyCollection<IPlant>>>;