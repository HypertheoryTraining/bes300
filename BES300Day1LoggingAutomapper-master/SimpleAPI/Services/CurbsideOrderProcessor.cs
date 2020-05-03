using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimpleAPI.Domain;
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

        public CurbsideOrderProcessor(ILogger<CurbsideOrderProcessor> logger, CurbsideChannel theChannel, IServiceProvider serviceProvider)
        {
            Logger = logger;
            TheChannel = theChannel;
            ServiceProvider = serviceProvider;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach(var orderId in TheChannel.ReadAllAsync())
            {
                using var scope = ServiceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<ShoppingDataContext>();

                var savedOrder = await context.Orders.SingleOrDefaultAsync(o => o.Id == orderId);
                // handle no order..
                var items = savedOrder.Items.Split(',').Count();
                for(var t = 0; t< items; t++)
                {
                    await Task.Delay(300); // wait 300 milliseconds for each item
                }
                savedOrder.Status = CurbsideOrderStatus.Approved;
                await context.SaveChangesAsync();
            }
        }
    }
}
