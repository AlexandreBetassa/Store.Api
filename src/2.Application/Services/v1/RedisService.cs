using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Project.CrossCutting.Configurations.v1;
using System.Text.Json;

namespace Store.Application.Services.v1
{
    public class RedisService(IDistributedCache cache, IOptions<Appsettings> appsettingsConfiguration) : IRedisService
    {
        private readonly IDistributedCache _cache = cache;
        private readonly Appsettings _appsettingsConfiguration = appsettingsConfiguration.Value;

        public async Task CreateAsync(string key, object data, int expirationInMinutes = 15)
        {
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(_appsettingsConfiguration.RedisConfiguration.ExpirationInMinutes)
            };

            var dataJson = JsonSerializer.Serialize(data);

            await _cache.SetStringAsync(key, dataJson, cacheOptions);
        }

        public async Task<string> GetKey(string key)
        {
            return await _cache.GetStringAsync(key) ?? string.Empty;
        }

        public async Task DeleteCache(string key) => await _cache.RemoveAsync(key);
    }
}