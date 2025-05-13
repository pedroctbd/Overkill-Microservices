using Confluent.Kafka;
using EventBus.Kafka;
using Microsoft.Extensions.Logging;

namespace NotificationService.Worker
{
    public class NotificationWorker : KafkaConsumerWorker<Ignore, string>
    {
        private readonly ILogger<NotificationWorker> _logger;

        public NotificationWorker(ILogger<NotificationWorker> logger)
            : base(
                topic: "notification-events",
                config: new ConsumerConfig
                {
                    BootstrapServers = "localhost:9092",
                    GroupId = "notification-service",
                    AutoOffsetReset = AutoOffsetReset.Earliest
                },
                logger: logger)
        {
            _logger = logger;
        }

        protected override async Task HandleMessageAsync(string message, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling notification message NotifcationWorker: {Message}", message);
        }
    }

}
