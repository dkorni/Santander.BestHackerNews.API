using Santander.BestHackerNews.Application.Constants;
using Santander.BestHackerNews.Domain;
using Santander.BestHackerNews.Persistence.Constants;
using Santander.BestHackerNews.Persistence.Dals;
using Santander.BestHackerNews.Persistence.Mappers;
using System.Net.Http.Json;

namespace Santander.BestHackerNews.Persistence.FetchStoryDataStrategies
{
    public abstract class FetchStoryDataStrategyBase
    {
        protected IHttpClientFactory _httpClientFactory;

        protected FetchStoryDataStrategyBase(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public abstract Task<Story[]> FetchAsync(int[] ids);

        protected async Task<Story> FetchStoryAsync(int id)
        {
            using var httpClient = _httpClientFactory.CreateClient(ApplicationConstants.HackerNews);

            var url = string.Format(UrlTemplates.ItemUrl, id);
            var message = await httpClient.GetAsync(url);

            var dal = await message.Content.ReadFromJsonAsync<StoryDal>();

            if (dal == null)
                throw new Exception();

            var story = dal.ToStory();

            return story;
        }
    }
}