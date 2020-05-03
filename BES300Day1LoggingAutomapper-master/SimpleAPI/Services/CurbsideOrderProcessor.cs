using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimpleAPI.Controllers;
using SimpleAPI.Domain;
using SimpleAPI.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleAPI.Services
{
    public class CurbsideOrderProcessor : BackgroundService
    {
        private readonly ILogger<CurbsideOrderProcessor> Logger;
        private readonly CurbsideChannel TheChannel;
        private readonly IServiceProvider ServiceProvider;
        private readonly IMapper Mapper;
        private readonly IHubContext<CurbsideHub> CurbsideHub;

        public CurbsideOrderProcessor(ILogger<CurbsideOrderProcessor> logger, CurbsideChannel theChannel, IServiceProvider serviceProvider, IMapper mapper, IHubContext<CurbsideHub> curbsideHub)
        {
            Logger = logger;
            TheChannel = theChannel;
            ServiceProvider = serviceProvider;
            Mapper = mapper;
            CurbsideHub = curbsideHub;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach(var order in TheChannel.ReadAllAsync())
            {
                using var scope = ServiceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<ShoppingDataContext>();
                
                var savedOrder = await context.Orders.SingleOrDefaultAsync(o => o.Id == order.OrderId);
                // handle no order..
                var items = savedOrder.Items.Split(',').Count();
                for(var t = 0; t< items; t++)
                {
                    await Task.Delay(300); // wait 300 milliseconds for each item
                }
                savedOrder.Status = CurbsideOrderStatus.Approved;
                await context.SaveChangesAsync();
                if(order.ClientId != null)
                {
                    //var hub = scope.ServiceProvider.GetRequiredService<IHubContext<CurbsideHub>>();
                    await CurbsideHub.Clients.Client(order.ClientId).SendAsync("OrderProcessed", Mapper.Map<CurbsideOrderResponse>(savedOrder));
                }
            }
        }
    }
}
