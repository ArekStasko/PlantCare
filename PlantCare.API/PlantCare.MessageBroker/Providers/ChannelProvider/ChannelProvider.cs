using Microsoft.Extensions.Logging;
using PlantCare.MessageBroker.Providers.ConnectionProvider;
using RabbitMQ.Client;

namespace PlantCare.MessageBroker.Providers.ChannelProvider;

public class ChannelProvider : IChannelProvider
{
    private readonly IConnectionProvider _connectionProvider;
    private readonly ILogger<ChannelProvider> _logger;
    private IModel _model;

    public ChannelProvider(IConnectionProvider connectionProvider, ILogger<ChannelProvider> logger, IModel model)
    {
        _connectionProvider = connectionProvider;
        _logger = logger;
        _model = model;
    }

    public IModel GetChannel()
    {
        if (_model == null || !_model.IsOpen)
        {
            _model = _connectionProvider.GetConnection().CreateModel();
        }

        return _model;
    }

    public void Dispose()
    {
        try
        {
            if (_model != null)
            {
                _model?.Close();
                _model?.Dispose();
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Cannot dispose RabbitMq connection");
        }
    }
}