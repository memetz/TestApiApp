using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
  public interface IWeatherForecastRepository
  {
    Task<IList<WeatherForecast>> GetAsync();
    Task<WeatherForecast> GetByIdAsync(long id);
    Task<WeatherForecast> AddAsync(WeatherForecast forecast);
    Task<WeatherForecast> UpdateAsync(long id, WeatherForecast forecast);
    Task<bool> DeleteAsync(long id);
  }
}
