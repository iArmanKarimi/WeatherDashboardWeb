namespace WeatherDashboard.Models
{
    public class WeatherDto
    {
        public string City { get; set; }
        public double TemperatureC { get; set; }
        public double WindSpeed { get; set; }
        public string ConditionCode { get; set; } // Optional: for icon mapping
    }
}

