using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EventBus.Kafka;

public abstract class KafkaConsumerWorker<TKey, TValue> : BackgroundService
{
    private readonly string _topic;
    private readonly ConsumerConfig _config;
    private readonly ILogger _logger;

    protected KafkaConsumerWorker(
        string topic,
        ConsumerConfig config,
        ILogger logger)
    {
        _topic = topic;
        _config = config;
        _logger = logger;
    }

    protected abstract Task HandleMessageAsync(TValue message, CancellationToken cancellationToken);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var consumer = new ConsumerBuilder<TKey, TValue>(_config).Build();
        consumer.Subscribe(_topic);
        _logger.LogInformation("Subscribed to topic {Topic}", _topic);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var result = consumer.Consume(stoppingToken);
                _logger.LogInformation("Consumed message: {Message}", result.Message.Value);
                await HandleMessageAsync(result.Message.Value, stoppingToken);
            }
            catch (ConsumeException ex)
            {
                _logger.LogError(ex, "Kafka consume error.");
            }
        }

        consumer.Close();
    }
}
