using LanguageExt.Common;
using MediatR;

namespace PlantCare.Queries.Abstraction.Queries.Plant;

public record GetPlantsQuery : IRequest<Result<IReadOnlyCollection<IPlant>>>;