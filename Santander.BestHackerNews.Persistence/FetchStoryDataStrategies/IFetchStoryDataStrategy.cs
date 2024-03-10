using Santander.BestHackerNews.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Santander.BestHackerNews.Persistence.FetchStoryDataStrategies
{
    public interface IFetchStoryDataStrategy
    {
        Task<Story[]> FetchAsync(int[] ids);
    }
}
