using Confluent.Kafka;

namespace EventBus.Kafka;

public class KafkaConfiguration
{
    public ProducerConfig ProducerConfig { get; set; }
    public ConsumerConfig ConsumerConfig { get; set; }
    public List<string> TopicsListened { get; set; }
    public string DefaultPublishTopic { get; set; }
    public List<string> AdditionalEventHandlerAssemblies { get; set; }

    public KafkaConfiguration()
    {
        TopicsListened = new();
    }
}
