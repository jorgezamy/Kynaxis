using Kynaxis.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace Kynaxis.Services
{
    public class HackerNewsService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private const string _baseURL = "https://hacker-news.firebaseio.com/v0/";

        public HackerNewsService(HttpClient httpClient, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
        }

        public async Task<List<int>> GetBestStoryIdsAsync()
        {
            var cacheKey = "beststories";
            var newList = new List<int>();

            if (!_cache.TryGetValue(cacheKey, out List<int>? storyIds))
            {
                var response = await _httpClient.GetStringAsync($"{_baseURL}{cacheKey}.json");
                storyIds = JsonSerializer.Deserialize<List<int>>(response) ?? newList;

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _cache.Set(cacheKey, storyIds, cacheEntryOptions);
            }

            return storyIds ?? newList;
        }

        public async Task<Story> GetStoryDetailsAsync(int storyId)
        {
            var response = await _httpClient.GetStringAsync($"{_baseURL}item/{storyId}.json");

            var story = JsonSerializer.Deserialize<Story>(response) ?? throw new NullReferenceException($"Failed to deserialize the story data for StoryID");

            return story;
        }
    }
}
