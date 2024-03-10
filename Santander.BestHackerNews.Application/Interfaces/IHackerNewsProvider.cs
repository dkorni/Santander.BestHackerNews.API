using Santander.BestHackerNews.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Santander.BestHackerNews.Application.Interfaces
{
    public interface IHackerNewsProvider
    {
        Task<Story[]> GetBestStories(int count);
    }
}
