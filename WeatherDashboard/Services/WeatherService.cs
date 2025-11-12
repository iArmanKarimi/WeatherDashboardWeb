using System.Net.Http;
using System.Text.Json;
using WeatherDashboard.Models;

public class WeatherService
{
    private readonly HttpClient _httpClient;

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherDto> GetWeatherAsync(string city)
    {
        // Step 1: Geocode city using Nominatim
        var geoUrl = $"https://nominatim.openstreetmap.org/search?format=json&q={city}";
        var geoResponse = await _httpClient.GetStringAsync(geoUrl);
        var geoJson = JsonDocument.Parse(geoResponse);
        var location = geoJson.RootElement[0];
        var lat = location.GetProperty("lat").GetString();
        var lon = location.GetProperty("lon").GetString();

        // Step 2: Call Open-Meteo
        var weatherUrl = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current_weather=true";
        var weatherResponse = await _httpClient.GetStringAsync(weatherUrl);
        var weatherJson = JsonDocument.Parse(weatherResponse);
        var current = weatherJson.RootElement.GetProperty("current_weather");

        return new WeatherDto
        {
            City = city,
            TemperatureC = current.GetProperty("temperature").GetDouble(),
            WindSpeed = current.GetProperty("windspeed").GetDouble(),
            ConditionCode = current.GetProperty("weathercode").GetRawText()
        };
    }
}
