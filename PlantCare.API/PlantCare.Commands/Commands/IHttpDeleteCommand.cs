using LanguageExt.Common;
using MediatR;

namespace PlantCare.Commands.Commands;

public interface IHttpDeleteCommand : IRequest<Result<bool>>;