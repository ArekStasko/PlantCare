using LanguageExt.Common;
using MediatR;
using PlantCare.API.DataAccess.Models;

namespace PlantCare.API.Services.Requests;

public record GetPlantsQuery : IRequest<Result<List<IPlant>>>
{
    
}