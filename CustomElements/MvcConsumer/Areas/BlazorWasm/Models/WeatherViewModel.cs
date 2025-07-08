namespace MvcConsumer.Areas.BlazorWasm.Models;

public class WeatherViewModel
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string? Summary { get; set; }

        public int TemperatureF { get; set; }
    }

    public WeatherForecast[]? Data { get; set; }
}
