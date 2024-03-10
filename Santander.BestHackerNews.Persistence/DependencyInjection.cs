using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Santander.BestHackerNews.Application.Constants;
using Santander.BestHackerNews.Application.Interfaces;
using Santander.BestHackerNews.Persistence.Constants;
using Santander.BestHackerNews.Persistence.FetchStoryDataStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Santander.BestHackerNews.Persistence
{
    public static class DependencyInjection
    {
        public static void AddPersistenceForReading(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient(ApplicationConstants.HackerNews, c =>
            {
                c.BaseAddress = new Uri(UrlTemplates.HackerNewsUrl);
            });

            services.AddStackExchangeRedisCache(options=>options.Configuration = configuration.GetConnectionString("Cache"));

            services.AddSingleton<FetchStoryDataStrategyBase, ParallelFetchStoryDatalStrategy>();
            services.AddSingleton<IHackerNewsProvider, HackerNewsHttpProvider>();
            services.Decorate<IHackerNewsProvider, RedisHackerNewsProvider>();
        }

        public static void AddPersistenceForWriting(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient(ApplicationConstants.HackerNews, c =>
            {
                c.BaseAddress = new Uri(UrlTemplates.HackerNewsUrl);
            });

            services.AddStackExchangeRedisCache(options => options.Configuration = configuration.GetConnectionString("Cache"));

            services.AddSingleton<FetchStoryDataStrategyBase, ParallelFetchStoryDatalStrategy>();
            services.AddSingleton<IHackerNewsLiveManager, HackerNewsLiveManager>();
        }
    }
}