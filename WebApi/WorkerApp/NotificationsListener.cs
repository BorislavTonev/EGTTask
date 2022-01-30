using EFCore.BulkExtensions;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Application.Interfaces;
using WebApi.Domain.Entities;

namespace WorkerApp
{
    public class NotificationsListener : IConsumer<NotifyItem>
    {
        private readonly ICacheService _cache;
        private readonly WorkerDbContext _workerDbContext;

        public NotificationsListener(ICacheService cacheService, WorkerDbContext workerDbContext)
        {
            _cache = cacheService;
            _workerDbContext = workerDbContext;
        }

        public async Task Consume(ConsumeContext<NotifyItem> context)
        {
            var obj = context.Message;
       
            List<ItemDbEntity> dbItems = new List<ItemDbEntity>();

            try
            {
                var redisItems = await _cache.GetAsync(obj.CacheKey);

                foreach (var item in redisItems)
                {
                    dbItems.Add(new ItemDbEntity() { ItemName = item.ItemName, Description = item.Description });
                }

                await _workerDbContext.BulkInsertAsync(dbItems);

            }
            catch (Exception){ }
          
        }
    }
}
