using Santander.BestHackerNews.Contracts;
using Santander.BestHackerNews.Domain;

namespace Santander.BestHackerNews.API.Extensions
{
    public static class StoryMapper
    {
        public static StoryDto ToDto(this Story story) =>
            new StoryDto(story.Title, story.Url, story.PostedBy, story.Time.ToString(), story.Score, story.ComentCount);
    }
}
