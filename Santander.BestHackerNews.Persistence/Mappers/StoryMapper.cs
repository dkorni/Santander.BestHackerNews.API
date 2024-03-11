using Santander.BestHackerNews.Domain;
using Santander.BestHackerNews.Persistence.Dals;

namespace Santander.BestHackerNews.Persistence.Mappers
{
    public static class StoryMapper
    {
        public static Story ToStory(this StoryDal dal)
        {
            return new Story
            {
                Id = dal.Id,
                Title = dal.Title,
                Url = dal.Url,
                PostedBy = dal.By,
                Time = new DateTime(dal.Time),
                Score = dal.Score,
                ComentCount = dal.Descendants
            };
        }
    }
}