using Santander.BestHackerNews.Application.Constants;
using Santander.BestHackerNews.Application.Interfaces;
using Santander.BestHackerNews.Domain;
using Santander.BestHackerNews.Persistence.Constants;
using Santander.BestHackerNews.Persistence.FetchStoryDataStrategies;
using System.Net.Http.Json;

namespace Santander.BestHackerNews.Persistence
{
    public class OriginHackerNewsProvider : IHackerNewsProvider
    {
        private IHttpClientFactory _httpClientFactory;
        private FetchStoryDataStrategyBase _fetchStoryDataStrategy;

        public OriginHackerNewsProvider(IHttpClientFactory httpClientFactory, FetchStoryDataStrategyBase fetchStoryDataStrategy)
        {
            _httpClientFactory = httpClientFactory;
            _fetchStoryDataStrategy = fetchStoryDataStrategy;
        }

        public async Task<Story[]> GetBestStories(int count)
        {
            using var httpClient = _httpClientFactory.CreateClient(ApplicationConstants.HackerNews);

            try
            {
               var stories = await GetBestStoriesInternal();

                return stories.Take(count).ToArray();
            }
            catch
            {
                return Array.Empty<Story>();
            }
        }

        public Task<Story[]> GetBestStories() => GetBestStoriesInternal();

        private async Task<Story[]> GetBestStoriesInternal()
        {
            var storyIds = await GetIds();

            var stories = await _fetchStoryDataStrategy.FetchAsync(storyIds);

            return stories.OrderByDescending(x => x.Score).ToArray();
        }

        private async Task<int[]> GetIds()
        {
            using var httpClient = _httpClientFactory.CreateClient(ApplicationConstants.HackerNews);

            var bestStoryIdsResponse = await httpClient.GetAsync(UrlTemplates.BestStoriesUrl);
            var bestStoryIds = await bestStoryIdsResponse.Content.ReadFromJsonAsync<int[]>();

            return bestStoryIds;
        }
    }
}