using LanguageExt.Common;
using MediatR;

namespace PlantCare.Commands.Abstraction.Commands;

public interface IHttpPostCommand : IRequest<Result<bool>>
{
}

public interface IHttpPostCommandGuid : IRequest<Result<Guid>>
{
}