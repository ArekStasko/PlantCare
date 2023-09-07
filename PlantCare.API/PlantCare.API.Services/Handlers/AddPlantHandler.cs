using LanguageExt.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess;
using PlantCare.API.Services.Requests;

namespace PlantCare.API.Services.Handlers;

public class AddPlantHandler : IRequestHandler<AddPlantRequest, Result<bool>>
{
    private readonly DataContext _dataContext;
    private readonly ILogger<AddPlantRequest> _logger;

    public AddPlantHandler(DataContext dataContext, ILogger<AddPlantRequest> logger)
    {
        _dataContext = dataContext;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(AddPlantRequest request, CancellationToken cancellationToken)
    {
        try
        {
            throw new NotImplementedException();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}