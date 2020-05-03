using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SimpleAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleAPI.Controllers;
using SimpleAPI.Services;

namespace SimpleAPI.Hubs
{
    public class CurbsideHub : Hub
    {
        private ShoppingDataContext DataContext;
        private ILogger<CurbsideHub> Logger;
        private IMapper Mapper;
        private readonly CurbsideChannel TheChannel;

        public CurbsideHub(ShoppingDataContext context, ILogger<CurbsideHub> logger, IMapper mapper, CurbsideChannel theChannel)
        {
            DataContext = context;
            Logger = logger;
            Mapper = mapper;
            TheChannel = theChannel;
        }

        public async Task PlaceOrder(CurbsideOrderRequest orderToBePlaced)
        {
            var order = Mapper.Map<CurbsideOrder>(orderToBePlaced);
            DataContext.Orders.Add(order);
            await DataContext.SaveChangesAsync();
            await TheChannel.AddCurbside(new CubsideChannelRequest { OrderId = order.Id, ClientId = Context.ConnectionId });
            await Clients.Caller.SendAsync("OrderPlaced", Mapper.Map<CurbsideOrderResponse>(order));
        }
    }
}
