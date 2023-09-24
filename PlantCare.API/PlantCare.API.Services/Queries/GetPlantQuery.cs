using LanguageExt.Common;
using MediatR;
using PlantCare.API.DataAccess.Models;

namespace PlantCare.API.Services.Requests;

public record GetPlantQuery : IRequest<Result<IPlant>>
{
    public int Id { get; set; }
}