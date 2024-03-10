using MediatR;
using Santander.BestHackerNews.Application.Interfaces;
using Santander.BestHackerNews.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Santander.BestHackerNews.Application.Queries
{
    public class GetBestStoriesQueryHandler : IRequestHandler<GetBestStoriesQuery, Story[]>
    {
        private readonly IHackerNewsProvider _hackerNewsProvider;

        public GetBestStoriesQueryHandler(IHackerNewsProvider hackerNewsProvider)
        {
            _hackerNewsProvider = hackerNewsProvider;
        }

        public Task<Story[]> Handle(GetBestStoriesQuery request, CancellationToken cancellationToken)
        {
            return _hackerNewsProvider.GetBestStories(request.Count);
        }
    }
}