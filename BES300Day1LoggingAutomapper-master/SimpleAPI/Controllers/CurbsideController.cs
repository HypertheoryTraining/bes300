using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleAPI.Domain;
using SimpleAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
namespace SimpleAPI.Controllers
{
    public class CurbsideController : ControllerBase
    {

        IMapper Mapper;
        ShoppingDataContext Context;
        private readonly CurbsideChannel theChannel;

        public CurbsideController(IMapper mapper, ShoppingDataContext context, CurbsideChannel theChannel)
        {
            Mapper = mapper;
            Context = context;
            this.theChannel = theChannel;
        }

        [HttpPost("curbside")]
        public async Task<ActionResult> ScheduleCurbsidePickup([FromBody] CurbsideOrderRequest orderRequest)
        {
            var order = Mapper.Map<CurbsideOrder>(orderRequest);
            order.Status = CurbsideOrderStatus.Pending;
            Context.Orders.Add(order);
            await Context.SaveChangesAsync();
            // TODO - Notify
            try
            {
                await theChannel.AddCurbside(new CubsideChannelRequest {  OrderId = order.Id });
            } catch (OperationCanceledException ex)
            {
                // cleanup;
            }
            return CreatedAtRoute("orders#getorder", new { orderId = order.Id },Mapper.Map<CurbsideOrderResponse>(order));
        }

        [HttpGet("curbside/{orderId:int}", Name ="orders#getorder")]
        public async Task<ActionResult> GetOrder(int orderId)
        {
            var order = await Context.Orders.SingleOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
            {
                return NotFound();
            } else
            {
                return Ok(Mapper.Map<CurbsideOrderResponse>(order));
            }
        }

    }

    public class CurbsideOrderRequest
    {
        public string For { get; set; }
        public string Items { get; set; }
    }
    

    public class CurbsideOrderResponse
    {
        public int Id { get; set; }
        public string For { get; set; }
        public string Items { get; set; }
       [JsonConverter(typeof(JsonStringEnumConverter))]
        public CurbsideOrderStatus Status { get; set; }
    }
}
