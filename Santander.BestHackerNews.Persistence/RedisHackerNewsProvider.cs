﻿using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Santander.BestHackerNews.Application.Interfaces;
using Santander.BestHackerNews.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Santander.BestHackerNews.Persistence
{
    public class RedisHackerNewsProvider : IHackerNewsProvider
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IHackerNewsProvider _originalNewsProvider;
        private readonly static string BestStories = nameof(BestStories);

        public RedisHackerNewsProvider(
            IHackerNewsProvider originalNewsProvider,
            IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
            _originalNewsProvider = originalNewsProvider;
        }

        public async Task<Story[]> GetBestStories(int count)
        {
            var stories = await GetBestStories();
            return stories.OrderByDescending(x => x.Score).Take(count).ToArray();
        }

        public async Task<Story[]> GetBestStories()
        {
            var cacheValue = await _distributedCache.GetAsync(BestStories);

            Story[] stories = null;

            if (cacheValue == null)
            {
                stories = await _originalNewsProvider.GetBestStories();

                var json = JsonConvert.SerializeObject(stories);
                var bytes = Encoding.UTF8.GetBytes(json);

                await _distributedCache.SetAsync(BestStories, bytes);
                return stories;
            }

            var cachedJson = Encoding.UTF8.GetString(cacheValue);
            stories = JsonConvert.DeserializeObject<Story[]>(cachedJson);
            return stories;
        }
    }
}