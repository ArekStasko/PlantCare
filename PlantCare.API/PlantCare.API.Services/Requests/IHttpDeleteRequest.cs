namespace PlantCare.API.Services.Requests;

using LanguageExt.Common;
using MediatR;

public interface IHttpDeleteRequest : IRequest<Result<bool>>
{
    
}