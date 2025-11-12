using System.Text.Json;
using WeatherDashboard.Models;

namespace WeatherDashboard.Services
{
    // Services/WeatherService.cs
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public WeatherService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _apiKey = config["OpenWeather:ApiKey"];
        }

        public async Task<WeatherDto> GetWeatherAsync(string city)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric";
            var response = await _httpClient.GetStringAsync(url);
            using var doc = JsonDocument.Parse(response);
            var root = doc.RootElement;

            return new WeatherDto
            {
                City = root.GetProperty("name").GetString(),
                TemperatureC = root.GetProperty("main").GetProperty("temp").GetDouble(),
                Condition = root.GetProperty("weather")[0].GetProperty("main").GetString(),
                Humidity = root.GetProperty("main").GetProperty("humidity").GetInt32(),
                WindSpeed = root.GetProperty("wind").GetProperty("speed").GetDouble()
            };
        }
    }

}
