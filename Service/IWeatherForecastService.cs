using customer.api.service.Model;

public interface IWeatherForecastService
{
    IEnumerable<WeatherForecast> Get();
}