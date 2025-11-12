namespace WeatherDashboard.Models
{
    public class WeatherDto
    {
        public string City { get; set; }
        public double TemperatureC { get; set; }
        public string Condition { get; set; }
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }


    }
}
