using MediatR;
using Microsoft.AspNetCore.Mvc;
using Santander.BestHackerNews.API.Extensions;
using Santander.BestHackerNews.Application.Queries;
using Santander.BestHackerNews.Contracts;

namespace Santander.BestHackerNews.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BestStoriesController : ControllerBase
    {
        private IMediator _mediator;

        public BestStoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetBestStories")]
        public async Task<StoryDto[]> Get([FromQuery] int count)
        {
            var getBestStoriesQuery = new GetBestStoriesQuery()
            {
                Count = count
            };

            var stories = await _mediator.Send(getBestStoriesQuery);

            var result = stories.Select(x=>x.ToDto()).ToArray();
            return result;
        }
    }
}