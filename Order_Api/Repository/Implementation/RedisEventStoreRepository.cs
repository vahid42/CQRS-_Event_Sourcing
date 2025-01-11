using System.Text.Json;
using System.Text.Json.Serialization;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Options;
using Order_Api.Events;
using StackExchange.Redis;

namespace Order_Api.Repository.Implementation
{
    public class RedisEventStoreRepository : IEventStoreRepository
    {
        private readonly IOptions<RedisConfig> _redisConfigOptions;
        private readonly ILogger<RedisEventStoreRepository> _logger;
        private readonly IDatabase _redisDatabase;


        public RedisEventStoreRepository(IOptions<RedisConfig> redisConfigOptions,
            ILogger<RedisEventStoreRepository> logger,
            IDatabase redisDatabase)
        {
            _redisConfigOptions = redisConfigOptions;
            _logger = logger;
            _redisDatabase = redisDatabase;
        }

        public async Task PublishAsync(IEvent message)
        {
            Guard.Against.Null(message);
            var @event = new[] { new NameValueEntry(message.GetType().FullName, JsonSerializer.Serialize(message)) };

            await _redisDatabase.StreamAddAsync(_redisConfigOptions.Value.StreamName, @event);
        }
    }
}
