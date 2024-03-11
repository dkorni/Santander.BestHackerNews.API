using Firebase.Database;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Santander.BestHackerNews.Application.Interfaces;
using Santander.BestHackerNews.Persistence.Constants;
using Santander.BestHackerNews.Persistence.FetchStoryDataStrategies;
using System.Text;

namespace Santander.BestHackerNews.Persistence
{
    public class HackerNewsLiveManager : IHackerNewsLiveManager
    {
        private FirebaseClient _firebaseClient;
        private readonly IDistributedCache _distributedCache;
        private readonly FetchStoryDataStrategyBase _fetchStoryDataStrategy;
        private readonly ILogger _logger;

        public HackerNewsLiveManager(IDistributedCache distributedCache, 
            FetchStoryDataStrategyBase fetchStoryDataStrategy,
            ILogger<HackerNewsLiveManager> logger)
        {
            _firebaseClient = new FirebaseClient(UrlTemplates.HackerNewsUrl);
            _distributedCache = distributedCache;
            _fetchStoryDataStrategy = fetchStoryDataStrategy;
            _logger = logger;
        }

        public void Start()
        {
            _firebaseClient
                .Child(FirebaseObjectNames.BestStories)
                .AsObservable<int[]>()
                .Subscribe(async e => await UpdateStoriesInCache(e.Object));

            _logger.LogInformation(nameof(HackerNewsLiveManager) + " was run successfully");
        }

        private async Task UpdateStoriesInCache(int[] ids)
        {
            var stories = await _fetchStoryDataStrategy.FetchAsync(ids);
            stories = stories.OrderByDescending(x => x.Score).ToArray();

            var json = JsonConvert.SerializeObject(stories);
            var bytes = Encoding.UTF8.GetBytes(json);

            await _distributedCache.SetAsync(CacheKeyNames.BestStories, bytes);

            _logger.LogInformation("Stories were updated in cache.");
        }

        public void Stop()
        {
            _firebaseClient.Dispose();
        }
    }
}