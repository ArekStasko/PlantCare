using LanguageExt.Common;
using MediatR;

namespace PlantCare.Commands.Abstraction.Commands;

public interface IHttpDeleteCommand : IRequest<Result<bool>>;