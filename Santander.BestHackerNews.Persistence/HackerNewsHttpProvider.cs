using Santander.BestHackerNews.Application.Constants;
using Santander.BestHackerNews.Application.Interfaces;
using Santander.BestHackerNews.Domain;
using Santander.BestHackerNews.Persistence.FetchStoryDataStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Santander.BestHackerNews.Persistence
{
    public class HackerNewsHttpProvider : IHackerNewsProvider
    {
        private IHttpClientFactory _httpClientFactory;
        private FetchStoryDataStrategyBase _fetchStoryDataStrategy;

        public HackerNewsHttpProvider(IHttpClientFactory httpClientFactory, FetchStoryDataStrategyBase fetchStoryDataStrategy)
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
            catch (Exception ex)
            {
                throw ex;
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

            var bestStoryIdsResponse = await httpClient.GetAsync("v0/beststories.json");
            var bestStoryIds = await bestStoryIdsResponse.Content.ReadFromJsonAsync<int[]>();

            if (bestStoryIds == null)
                throw new Exception();

            return bestStoryIds;
        }
    }
}