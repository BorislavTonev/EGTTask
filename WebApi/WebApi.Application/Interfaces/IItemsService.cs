using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Entities;

namespace WebApi.Application.Interfaces
{
    public interface IItemsService
    {
        public  Task<bool> AddItemsAsync(RedisItem item);
        public Task<List<Item>> GetAsync(string key);
        public void ItemsAdded();
    }
}
