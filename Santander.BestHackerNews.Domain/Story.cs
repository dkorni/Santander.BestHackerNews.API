﻿namespace Santander.BestHackerNews.Domain
{
    public class Story
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string PostedBy { get; set; }
        public DateTime Time { get; set; }
        public int Score { get; set; }
        public int ComentCount { get; set; }
    }
}
