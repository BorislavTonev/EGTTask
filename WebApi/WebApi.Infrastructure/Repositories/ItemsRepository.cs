using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Application.Interfaces;
using WebApi.Domain.Interfaces;

namespace WebApi.Infrastructure.Repositories
{
    public class ItemsRepository : IItemsRepository
    {
        private readonly ICacheService _cache;

        public ItemsRepository(ICacheService cache)
        {
            _cache = cache;
        }

        public bool AddItemsToRedis()
        {
            
            throw new NotImplementedException();
        }

    }
}
