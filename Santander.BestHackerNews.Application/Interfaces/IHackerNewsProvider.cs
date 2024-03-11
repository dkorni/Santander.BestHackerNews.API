using Santander.BestHackerNews.Domain;

namespace Santander.BestHackerNews.Application.Interfaces
{
    public interface IHackerNewsProvider
    {
        Task<Story[]> GetBestStories();

        Task<Story[]> GetBestStories(int count);
    }
}
