using Santander.BestHackerNews.Application.Constants;
using Santander.BestHackerNews.Domain;
using Santander.BestHackerNews.Persistence.Constants;
using Santander.BestHackerNews.Persistence.Dals;
using Santander.BestHackerNews.Persistence.Mappers;
using System.Net.Http.Json;

namespace Santander.BestHackerNews.Persistence.FetchStoryDataStrategies
{
    public class ParallelFetchStoryDatalStrategy : FetchStoryDataStrategyBase
    {
        public ParallelFetchStoryDatalStrategy(IHttpClientFactory httpClientFactory) : base(httpClientFactory) { }

        public async override Task<Story[]> FetchAsync(int[] ids)
        {
            var tasks = ids.Select(FetchStoryAsync).ToArray();
            await Task.WhenAll(tasks);

            var result = tasks.Select(x=>x.Result).ToArray();

            return result;
        }
    }
}