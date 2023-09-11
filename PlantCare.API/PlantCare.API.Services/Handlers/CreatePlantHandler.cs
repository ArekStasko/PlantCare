namespace PlantCare.API.Services.Handlers;

using AutoMapper;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Models;
using PlantCare.API.DataAccess.Repositories.PlantRepository;
using PlantCare.API.Services.Requests;

public class CreatePlantHandler : IRequestHandler<CreatePlantRequest, Result<bool>>
{
    private readonly IPlantRepository _plantRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreatePlantRequest> _logger;

    public CreatePlantHandler(IPlantRepository plantRepository, IMapper mapper, ILogger<CreatePlantRequest> logger)
    {
        _plantRepository = plantRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Result<bool>> Handle(CreatePlantRequest request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("AddPlantHandler handles request");
            var plantToCreate = _mapper.Map<IPlant>(request);
            var result = await _plantRepository.Create(plantToCreate);
            return result.Match(succ =>
            {
                if (succ)
                {
                    return new Result<bool>(true);
                }

                return new Result<bool>(new Exception("Something went wrong"));
            }, err => new Result<bool>(err));
        }
        catch (Exception e)
        {
            _logger.LogError("Exception has been thrown in AddPlantHandler: {exception}", e.Message);
            return new Result<bool>(e);
        }
    }
}