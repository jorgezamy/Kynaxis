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
            if (n < 0)
            {
                return BadRequest("The value of 'n' needs to be a positive number");
            }

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
                        Url = story.Url,
                        By = story.By,
                        Time = story.Time,
                        Score = story.Score,
                        Descendants = story.Descendants,
                        Kids = story.Kids
                    });
                }
            });

            await Task.WhenAll(tasks);

            stories = stories.OrderByDescending(s => s.Score).ToList();

            return Ok(stories);
        }

    }
}
