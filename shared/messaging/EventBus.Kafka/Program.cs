using Confluent.Kafka;

namespace EventBus.Kafka;

class Program
{
    const string BootstrapServers = "localhost:9092";
    const string Topic = "notification-events";

    static async Task Main()
    {
        Console.WriteLine("Producing...");
        await ProduceMessage();

        //Console.WriteLine("Consuming...");
        //ConsumeMessages();
    }

    static async Task ProduceMessage()
    {
        var config = new ProducerConfig { BootstrapServers = BootstrapServers };

        using var producer = new ProducerBuilder<Null, string>(config).Build();
        var result = await producer.ProduceAsync(Topic, new Message<Null, string> { Value = "Hello Kafka!" });

        Console.WriteLine($"Produced message to: {result.TopicPartitionOffset}");
    }

    static void ConsumeMessages()
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = BootstrapServers,
            GroupId = "test-consumer-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe(Topic);

        var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true;
            cts.Cancel();
        };

        try
        {
            while (!cts.Token.IsCancellationRequested)
            {
                var cr = consumer.Consume(cts.Token);
                Console.WriteLine($"Consumed message: {cr.Message.Value}");
            }
        }
        catch (OperationCanceledException)
        {
            consumer.Close();
        }
    }
}
