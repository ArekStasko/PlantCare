using LanguageExt.Common;
using MediatR;
using PlantCare.API.DataAccess.Models.Module;
using PlantCare.API.Services.Responses;

namespace PlantCare.API.Services.Queries.ModuleQueries;

public record GetModulesQuery : IRequest<Result<IReadOnlyCollection<GetModulesResponse>>>;