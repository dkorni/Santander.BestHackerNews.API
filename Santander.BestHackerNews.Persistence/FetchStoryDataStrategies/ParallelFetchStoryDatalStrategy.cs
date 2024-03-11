using Santander.BestHackerNews.Domain;

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