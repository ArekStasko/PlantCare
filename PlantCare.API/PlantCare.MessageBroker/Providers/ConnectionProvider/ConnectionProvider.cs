using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace PlantCare.MessageBroker.Providers.ConnectionProvider;

public class ConnectionProvider : IConnectionProvider
{
    private readonly ILogger<ConnectionProvider> _logger;
    private readonly IAsyncConnectionFactory _connectionFactory;
    private IConnection _connection;

    public ConnectionProvider(ILogger<ConnectionProvider> logger, IAsyncConnectionFactory connectionFactory)
    {
        _logger = logger;
        _connectionFactory = connectionFactory;
    }

    public void Dispose()
    {
        try
        {
            if (_connection != null)
            {
                _connection?.Close();
                _connection?.Dispose();
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Cannot dispose RabbitMq Connection");
        }
    }

    public IConnection GetConnection()
    {
        if (_connection == null || !_connection.IsOpen)
        {
            _connection = _connectionFactory.CreateConnection();
        }

        return _connection;
    }
}