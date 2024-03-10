using MediatR;
using Santander.BestHackerNews.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Santander.BestHackerNews.Application.Queries
{
    public class GetBestStoriesQuery : IRequest<Story[]>
    {
        public int Count { get; set; }
    }
}
