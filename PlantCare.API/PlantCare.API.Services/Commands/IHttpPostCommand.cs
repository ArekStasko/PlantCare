using LanguageExt.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace PlantCare.API.Services.Requests;

public interface IHttpPostCommand : IRequest<Result<bool>>
{
}

public interface IHttpPostCommandGuid : IRequest<Result<Guid>>
{
}