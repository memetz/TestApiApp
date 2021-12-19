using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
  public class WeatherForecastService : IWeatherForecastService
  {
    private IWeatherForecastRepository _repo;

    public WeatherForecastService(IWeatherForecastRepository repo )
    {
      _repo = repo;
    }

    public async Task<IList<WeatherForecast>> GetAsync()
    {

      return await _repo.GetAsync();
    }

    public async Task<WeatherForecast> GetByIdAsync(long id)
    {
      return await _repo.GetByIdAsync(id);
    }
    public async Task<WeatherForecast> AddAsync(WeatherForecast forecast)
    {
      return await _repo.AddAsync(forecast);
    }
    public async Task<WeatherForecast> UpdateAsync(long id, WeatherForecast forecast)
    {
      return await _repo.UpdateAsync(id, forecast);
    }
    public async Task<bool> DeleteAsync(long id)
    {
      return await _repo.DeleteAsync(id);
    }

    public ResponseEnum ValidateGetById(WeatherForecast forecast)
    {
      //TODO extra validation on top of swagger
      //if request is invalid return 400

      return ResponseEnum.Success;
    }
  }
}
