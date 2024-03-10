using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Santander.BestHackerNews.Persistence.Dals
{
    public class StoryDal
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string By { get; set; }
        public long Time { get; set; }
        public int Score { get; set; }
        public int ComentCount { get; set; }
    }
}