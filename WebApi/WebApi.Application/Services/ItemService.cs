using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.Interfaces;
using WebApi.Domain.Entities;

namespace WebApi.Application.Services
{
    public class ItemService : IItemsService
    {
        private readonly ICacheService _cache;
        

        public ItemService(ICacheService cache)
        {
            _cache = cache;
        }

        public Task<List<Item>> GetAsync(string key)
        {
            return _cache.GetAsync(key); 
        }

        public Task<bool> AddItemsAsync( RedisItem items)
        {
            return _cache.SetAsync(items);                      
        }

   
        public void ItemsAdded()
        {
            throw new NotImplementedException();
        }
    }
}
