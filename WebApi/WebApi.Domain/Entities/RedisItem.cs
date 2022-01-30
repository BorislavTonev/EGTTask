using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Domain.Entities
{
    public class RedisItem
    {
        public string RedisKey { get; set; }
        public List<Item> RedisValue { get; set; }
    }
}
