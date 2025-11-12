using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeatherDashboard.Models;

namespace WeatherDashboard.Pages
{
    public class IndexModel : PageModel
    {
        private readonly WeatherService _weatherService;

        public IndexModel(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [BindProperty]
        public string City { get; set; }

        public WeatherDto Weather { get; set; }

        public async Task OnPostAsync()
        {
            if (!string.IsNullOrWhiteSpace(City))
            {
                Weather = await _weatherService.GetWeatherAsync(City);
            }
        }
    }

}
