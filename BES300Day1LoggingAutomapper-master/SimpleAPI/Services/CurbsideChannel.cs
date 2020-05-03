using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace SimpleAPI.Services
{
    public class CurbsideChannel
    {
        private const int MaxMessagesInChannel = 100;
        private readonly Channel<CubsideChannelRequest> TheChannel;
        private readonly ILogger<CurbsideChannel> Logger;

        public CurbsideChannel(ILogger<CurbsideChannel> logger)
        {
            Logger = logger;
            var options = new BoundedChannelOptions(MaxMessagesInChannel)
            {
                SingleWriter = false,
                SingleReader = true
            };
            TheChannel = Channel.CreateBounded<CubsideChannelRequest>(options);

        }

        public async Task<bool> AddCurbside(CubsideChannelRequest order, CancellationToken ct = default)
        {
            while( await TheChannel.Writer.WaitToWriteAsync(ct) && !ct.IsCancellationRequested)
            {
                if(TheChannel.Writer.TryWrite(order))
                {
                    return true;
                } 
            }

            return false;
        }

        public IAsyncEnumerable<CubsideChannelRequest> ReadAllAsync(CancellationToken ct = default) => TheChannel.Reader.ReadAllAsync(ct);
    }
}
