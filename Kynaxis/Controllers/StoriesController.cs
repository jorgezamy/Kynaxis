using Kynaxis.Models;
using Kynaxis.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kynaxis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoriesController : Controller
    {
        private readonly HackerNewsService _hackerNewsService;

        public StoriesController(HackerNewsService hackerNewsService)
        {
            _hackerNewsService = hackerNewsService;
        }

        [HttpGet("best")]
        public async Task<IActionResult> GetBestStories([FromQuery] int n = 10)
        {
            var bestStoryId = await _hackerNewsService.GetBestStoryIdsAsync();

            var stories = new List<Story>();

            var tasks = bestStoryId.Take(n).Select(async id =>
            {
                var story = await _hackerNewsService.GetStoryDetailsAsync(id);

                if (story != null)
                {
                    stories.Add(new Story
                    {
                        Title = story.Title,
                        Uri = story.Uri,
                        PostedBy = story.PostedBy,
                        Time = DateTimeOffset.FromUnixTimeSeconds(long.Parse(story.Time)).ToString("yyyy-MM-ddTHH:mm:ss+00:00"),
                        Score = story.Score,
                        CommentCount = story.CommentCount
                    });
                }
            });

            await Task.WhenAll(tasks);

            stories = stories.OrderByDescending(s => s.Score).ToList();

            return Ok(stories);
        }

    }
}
