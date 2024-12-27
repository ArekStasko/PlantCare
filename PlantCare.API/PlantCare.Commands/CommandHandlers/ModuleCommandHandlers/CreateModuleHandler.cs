using AutoMapper;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Commands.Commands.Module;
using PlantCare.Domain.Dto;
using PlantCare.MessageBroker.Messages;
using PlantCare.MessageBroker.Producer;
using PlantCare.Persistance.WriteDataManager.Repositories;

namespace PlantCare.Commands.CommandHandlers.ModuleCommandHandlers;

public class CreateModuleHandler : IRequestHandler<CreateModuleCommand, Result<bool>>
{
    private readonly ModuleRepository _repository;
    private readonly IMapper _mapper;
    private readonly IQueueProducer<Module> _producer;
    private readonly ILogger<CreateModuleHandler> _logger;

    public CreateModuleHandler(ModuleRepository repository, IMapper mapper, IQueueProducer<Module> producer, ILogger<CreateModuleHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _producer = producer;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(CreateModuleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CreateModuleHandler start processing");
        var result = await _repository.Add(request.UserId, request.Id);
        return result.Match(succ =>
        {
            
        }, err =>
        {
            _logger.LogError("CreateModuleHandler error: {err}", err);
        });
    }
}