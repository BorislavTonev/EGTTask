using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Entities;

namespace WebApi.Application.Interfaces
{
    public interface ICacheService
    {
        Task<List<Item>> GetAsync(string key);
        Task<bool> SetAsync(RedisItem value);
    }
}
