using Microsoft.Extensions.Hosting;
using Santander.BestHackerNews.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Santander.BestHackerNews.LiveManagerHost
{
    public class LiveManagerHostService : IHostedService
    {
        private readonly IHackerNewsLiveManager _hackerNewsLiveManager;

        public LiveManagerHostService(IHackerNewsLiveManager hackerNewsLiveManager)
        {
            _hackerNewsLiveManager = hackerNewsLiveManager;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _hackerNewsLiveManager.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _hackerNewsLiveManager.Stop();
            return Task.CompletedTask;
        }
    }
}
