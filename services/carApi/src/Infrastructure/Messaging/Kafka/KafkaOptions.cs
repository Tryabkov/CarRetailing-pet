using Confluent.Kafka;

namespace Infrastructure.Messaging.Kafka;

public class KafkaOptions
{
    public string Topic { get; set; }
    public string BootstrapServers { get; set; }
    public Acks Acks { get; set; }
    public bool Idempotence { get; set; }
}