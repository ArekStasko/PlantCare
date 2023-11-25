namespace PlantCare.API.Services.Requests;

using LanguageExt.Common;
using MediatR;

public interface IHttpDeleteCommand : IRequest<Result<bool>>
{
}