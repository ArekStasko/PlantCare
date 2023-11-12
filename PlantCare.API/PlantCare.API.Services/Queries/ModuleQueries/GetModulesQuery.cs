using LanguageExt.Common;
using MediatR;
using PlantCare.API.DataAccess.Models.Module;

namespace PlantCare.API.Services.Queries.ModuleQueries;

public record GetModulesQuery : IRequest<Result<IReadOnlyCollection<IModule>>>;