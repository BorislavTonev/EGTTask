using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.Interfaces;
using WebApi.Domain.Entities;

namespace WebApi.Application.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<List<Item>> GetAsync(string key)
        {
            var byteResult = _cache.GetStringAsync(key);


            return await Task.Run(() => JsonConvert.DeserializeObject<List<Item>>(byteResult.Result));
        }

        public async Task<bool> SetAsync(RedisItem items)
        {
            try
            {
                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
                };

                var asString = JsonConvert.SerializeObject(items.RedisValue);

                await _cache.SetStringAsync(items.RedisKey, asString);

                return true;
            }
            catch (Exception)
            {
                return false;
            }             
        }
    }
}
