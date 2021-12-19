using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
  public class WeatherForecastRepository: IWeatherForecastRepository
  {
    private long count = 1;
    private static readonly string[] Summaries = new[]
    {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    private static readonly string[] Cities = new[]
    {
            "Madrid", "Rome", "London", "Sydney", "New York", "LA", "Melbourne", "Moscow", "Paris", "Berlin"
    };
    private IList<WeatherForecast> _wfl = null;
    public WeatherForecastRepository()
    {
      if (_wfl == null)
      {
        var rng = new Random();
        _wfl = Enumerable.Range(1, 6).Select(index => new WeatherForecast
        {
          Id = count++,
          Date = DateTime.Now.AddDays(index),
          TemperatureC = rng.Next(-20, 55),
          Summary = Summaries[rng.Next(Summaries.Length)],
          City = Cities[rng.Next(Cities.Length)]
        })
        .ToList();
      }
    }

    public async Task<IList<WeatherForecast>> GetAsync()
    {
      return await Task.FromResult(_wfl);
    }

    public async Task<WeatherForecast> GetByIdAsync(long id)
    {
      return await Task.FromResult(_wfl.FirstOrDefault(w => w.Id == id));
    }
    public async Task<WeatherForecast> AddAsync(WeatherForecast forecast)
    {
      forecast.Id = count++;
      _wfl.Add(forecast);
      return await GetByIdAsync(forecast.Id);
    }
    public async Task<WeatherForecast> UpdateAsync(long id, WeatherForecast forecast)
    {
      //updates only certain fields
      var exFor = await GetByIdAsync(id);
      if (exFor != null)
      {
        exFor.Summary = forecast.Summary;
        exFor.TemperatureC = forecast.TemperatureC;
        exFor.City = forecast.City;
      }
      return exFor;
    }
    public async Task<bool> DeleteAsync(long id)
    {
      var item = await GetByIdAsync(id);
      var res = _wfl.Remove(item);
      return await Task.FromResult(res);
    }
  }
}
