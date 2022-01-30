using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.Interfaces;
using WebApi.Domain.Entities;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RedisController : ControllerBase
    {

        private readonly IItemsService _itemsService;
        private readonly IBus _busService;

        public RedisController(IItemsService itemsService, IBus busService)
        {
            _itemsService = itemsService;
            _busService = busService;
        }

        [HttpGet]
        [Route("GetItemAsync")]
        public async Task<IActionResult> GetItemAsync(string cacheKey)
        {
            try
            {
                var cachedData = await _itemsService.GetAsync(cacheKey);

                return Ok(cachedData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("NotifyWorker")]
        public async Task<IActionResult> NotifyWorker(string cacheKey)
        {
            try
            {
                Uri uri = new Uri("rabbitmq://localhost/orderQueueasda");
                var endPoint = await _busService.GetSendEndpoint(uri);

                await endPoint.Send(new NotifyItem { CacheKey =cacheKey});

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddItemsAsync")]
        public async Task<ActionResult<bool>> AddItemsAsync([FromBody] RedisItem itemData)
        {
            try
            {
                var result = await _itemsService.AddItemsAsync(itemData);
                if (result)
                {
                    //Uri uri = new Uri("rabbitmq://localhost/vhost1/exchange1?bind=true&queue=queue1");
                    Uri uri = new Uri("rabbitmq://localhost/orderQueueasda");
                    var endPoint = await _busService.GetSendEndpoint(uri);                 
                    await endPoint.Send(itemData);
                }
             
                return Ok(result);
            }
            catch (Exception ex)
            {              
                return BadRequest(ex.Message);
            }
        }
    }
}
