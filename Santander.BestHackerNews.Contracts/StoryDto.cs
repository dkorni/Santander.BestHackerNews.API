namespace Santander.BestHackerNews.Contracts
{
    public record StoryDto(string Title, string Url, string PostedBy, string Time, int Score, int CommentCount);
}