using Santander.BestHackerNews.Application.Constants;
using Santander.BestHackerNews.Domain;
using Santander.BestHackerNews.Persistence.Constants;
using Santander.BestHackerNews.Persistence.Dals;
using Santander.BestHackerNews.Persistence.Mappers;
using System.Net.Http.Json;

namespace Santander.BestHackerNews.Persistence.FetchStoryDataStrategies
{
    public class SequentialFetchStoryDatalStrategy : IFetchStoryDataStrategy
    {
        private IHttpClientFactory _httpClientFactory;

        public SequentialFetchStoryDatalStrategy(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Story[]> FetchAsync(int[] ids)
        {
            using var httpClient = _httpClientFactory.CreateClient(ApplicationConstants.HackerNews);

            var list = new List<Story>(ids.Length);

            foreach (var id in ids)
            {
                try
                {
                    var url = string.Format(UrlTemplates.ItemUrl, id);
                    var message = await httpClient.GetAsync(url);

                    var dal = await message.Content.ReadFromJsonAsync<StoryDal>();

                    if (dal == null)
                        throw new Exception();

                    var story = dal.ToStory();
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