using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
  public interface IWeatherForecastService
  {
    Task<IList<WeatherForecast>> GetAsync();
    Task<WeatherForecast> GetByIdAsync(long id);
    Task<WeatherForecast> AddAsync(WeatherForecast WeatherForecast);
    Task<WeatherForecast> UpdateAsync(long id, WeatherForecast WeatherForecast);
    Task<bool> DeleteAsync(long id);
    ResponseEnum ValidateGetById(WeatherForecast forecast);
  }
}
