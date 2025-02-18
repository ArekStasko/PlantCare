using LanguageExt.Common;
using MediatR;

namespace PlantCare.Commands.Commands;

public interface IHttpPostCommand : IRequest<Result<bool>>
{
}

public interface IHttpPostCommandId : IRequest<Result<int>>
{
}