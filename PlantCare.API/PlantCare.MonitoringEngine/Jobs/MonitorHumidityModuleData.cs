using Coravel.Invocable;
using Microsoft.Extensions.Logging;
using PlantCare.MessageBroker.Messages;
using PlantCare.MessageBroker.Producer;
using PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;

namespace PlantCare.MonitoringEngine.Jobs;

public class MonitorHumidityModuleData(
    IQueueProducer<Module> producer,
    IWriteModuleRepository repository,
    ILogger<MonitorHumidityModuleData> logger
    ) : IInvocable
{
    
    public Task Invoke()
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