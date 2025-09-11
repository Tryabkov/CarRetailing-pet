using Microsoft.Extensions.Configuration;

namespace Infrastructure.Messaging.Kafka;

public class KafkaTopicResolver(IConfiguration config) : IKafkaTopicResolver
{
    public KafkaOptions Resolve<T>()
    {
        var section = config.GetSection($"Kafka:{typeof(T).Name}");

        return section.Get<KafkaOptions>() 
               ?? throw new Exception($"Kafka options not found: {typeof(T).Name}");

    }
}

public interface IKafkaTopicResolver
{
    KafkaOptions Resolve<T>();
}