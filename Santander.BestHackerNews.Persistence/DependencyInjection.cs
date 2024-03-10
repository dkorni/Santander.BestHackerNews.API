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
        public static void AddPersistence(this IServiceCollection services)
        {
            services.AddHttpClient(ApplicationConstants.HackerNews, c =>
            {
                c.BaseAddress = new Uri(UrlTemplates.HackerNewsUrl);
            });

            services.AddSingleton<IFetchStoryDataStrategy, SequentialFetchStoryDatalStrategy>();
            services.AddSingleton<IHackerNewsProvider, HackerNewsHttpProvider>();
        }
    }
}