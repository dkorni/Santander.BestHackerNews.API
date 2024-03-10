using Santander.BestHackerNews.Application.Constants;
using Santander.BestHackerNews.Domain;
using Santander.BestHackerNews.Persistence.Constants;
using Santander.BestHackerNews.Persistence.Dals;
using Santander.BestHackerNews.Persistence.Mappers;
using System.Net.Http.Json;

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
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return list.ToArray();
        }
    }
}