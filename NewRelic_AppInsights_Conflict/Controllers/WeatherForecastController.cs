using Microsoft.AspNetCore.Mvc;

namespace NewRelic_AppInsights_Conflict.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        const string baseURL = "https://jsonplaceholder.typicode.com/";

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var client = new HttpClient();
            await client.GetAsync($"{baseURL}");
            await client.PostAsJsonAsync($"{baseURL}posts", "{ \"phone\": \"+1 (975) 458-3685\"}");
            await client.PutAsJsonAsync($"{baseURL}posts/1", "{ \"phone\": \"+1 (975) 458-3685\"}");
            await client.PatchAsJsonAsync($"{baseURL}posts/1", "{ \"phone\": \"+1 (975) 458-3685\"}");
            await client.DeleteAsync($"{baseURL}posts/1");
            await client.GetAsync($"{baseURL}posts");
            await client.GetAsync("https://google.com");
            Console.WriteLine("All HTTP requests finished");

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
