using LanguageExt.Common;
using MediatR;
using PlantCare.API.DataAccess.Models;

namespace PlantCare.API.Services.Requests;

public class IHttpGetRequest : IRequest<Result<List<IPlant>>>, IRequest<Result<IPlant>>
{
    
}