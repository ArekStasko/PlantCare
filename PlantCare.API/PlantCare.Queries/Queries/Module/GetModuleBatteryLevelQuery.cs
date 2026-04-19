using LanguageExt.Common;
using MediatR;

namespace PlantCare.Queries.Queries.Module;

public class GetModuleBatteryLevelQuery: IRequest<Result<int>>
{
    public int UserId { get; set; }
    public int ModuleId { get; set; }
}