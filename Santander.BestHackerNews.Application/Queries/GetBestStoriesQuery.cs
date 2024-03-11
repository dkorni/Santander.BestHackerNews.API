using MediatR;
using Santander.BestHackerNews.Domain;

namespace Santander.BestHackerNews.Application.Queries
{
    public class GetBestStoriesQuery : IRequest<Story[]>
    {
        public int Count { get; set; }
    }
}
