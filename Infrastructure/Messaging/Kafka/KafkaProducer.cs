using Application.Interfaces;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Messaging.Kafka;

public class KafkaProducer<TMessage> : IEventBus<TMessage>
{
    private readonly IProducer<string, TMessage> _producer;
    private readonly string _topic;

    public KafkaProducer(IKafkaTopicResolver resolver, ILogger<KafkaProducer<TMessage>> logger)
    {
        var options = resolver.Resolve<TMessage>();
        var config = new ProducerConfig
        {
            BootstrapServers = options.BootstrapServers,
            Acks = options.Acks,
            EnableIdempotence = options.Idempotence,
            MessageTimeoutMs = 5000,
            ReconnectBackoffMs = 5000,
            ReconnectBackoffMaxMs = 60000
        };

        _producer = new ProducerBuilder<string, TMessage>(config)
            .SetValueSerializer(new KafkaJsonSerializer<TMessage>())
            .SetErrorHandler((p,e) => logger.LogError($"Kafka producer error: {e.Reason}"))
            .Build();
        
        _topic = options.Topic;
    }
    
    public async Task PublishAsync(TMessage message, CancellationToken ct)
    {
        try
        {
            await _producer.ProduceAsync(_topic, new  Message<string, TMessage>
            {
                Value = message
            }, ct);
        }
        catch (ProduceException<string, TMessage> e)
        {
            Console.WriteLine($"[Kafka] Delivery failed: {e.Error.Reason}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Kafka] Unexpected error: {ex.Message}");
        }

        
    }

    public void Dispose()
    {
        _producer?.Dispose();
    }
    
}   