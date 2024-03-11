using Santander.BestHackerNews.Domain;

namespace Santander.BestHackerNews.Persistence.FetchStoryDataStrategies
{
    public class SequentialFetchStoryDatalStrategy : FetchStoryDataStrategyBase
    {
        public SequentialFetchStoryDatalStrategy(IHttpClientFactory httpClientFactory) : base(httpClientFactory) { }

        public async override Task<Story[]> FetchAsync(int[] ids)
        {
            var list = new List<Story>(ids.Length);

            foreach (var id in ids)
            {
                try
                {
                    var story = await FetchStoryAsync(id);
                    list.Add(story);
                }
                catch
                {                   
                }
            }

            return list.ToArray();
        }
    }
}