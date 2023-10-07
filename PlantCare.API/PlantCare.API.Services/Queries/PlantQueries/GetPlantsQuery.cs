namespace PlantCare.API.Services.Requests;

using LanguageExt.Common;
using MediatR;
using PlantCare.API.DataAccess.Models;

public record GetPlantsQuery : IRequest<Result<List<IPlant>>>;