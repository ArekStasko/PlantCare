namespace PlantCare.API.Services.Handlers;

using AutoMapper;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Repositories.PlantRepository;
using PlantCare.API.Services.Requests;
using Serilog;
using ILogger = Serilog.ILogger;
public class DeletePlantHandler : IRequestHandler<DeletePlantRequest, Result<bool>>
{
    private readonly IPlantRepository _plantRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<DeletePlantHandler> _logger;
    
    public DeletePlantHandler(IPlantRepository plantRepository, IMapper mapper, ILogger<DeletePlantHandler> logger)
    {
        _plantRepository = plantRepository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public Task<Result<bool>> Handle(DeletePlantRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}