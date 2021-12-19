using AutoMapper;
using BusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication
{
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherForecastService _wfs;
    private readonly IMapper _mapper;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService wfs, IMapper mapper)
    {
      _logger = logger;
      _wfs = wfs;
      _mapper = mapper;
    }

    /// <summary>
    /// Returns all the weather forecasts.
    /// </summary>
    /// <returns>List of weather forecasts</returns>
    [HttpGet("", Name = nameof(GetAsync))]
    [ProducesResponseType(typeof(IList<WeatherForecast>), 200)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAsync()
    {
      //var res = Mapper.Map<IList<WeatherForecast>>(_wfs.GetWeatherForecast());
      var forecast = await _wfs.GetAsync();
      var res = _mapper.Map<IList<WeatherForecast>>(forecast);
      return Ok(MapResponseObject<IList<WeatherForecast>>(res));
    }

    /// <summary>
    /// Creates a WeatherForecast.
    /// </summary>
    /// <returns>bool</returns>
    [HttpPost("", Name = nameof(PostAsync))]
    [ProducesResponseType(typeof(WeatherForecast), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostAsync(WeatherForecast request)
    {
      var forecastReq = MapRequestObject<BusinessLogic.WeatherForecast>(request, out Response response);

      if (response != null || request == null)
        return BadRequest(response);

      var res = await _wfs.AddAsync(forecastReq);

      return Ok(MapResponseObject<WeatherForecast>(res));
    }

    /// <summary>
    /// Returns a WeatherForecast.
    /// </summary>
    /// <returns>WeatherForecast</returns>
    [HttpGet("{id:long:min(1)}", Name = nameof(GetByIdAsync))]
    [ProducesResponseType(typeof(WeatherForecast), 200)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(long id)
    {
      var res = await _wfs.GetByIdAsync(id);
      if (res == null)
        return NotFound();

      return Ok(res);
    }

    /// <summary>
    /// Updates a WeatherForecast.
    /// </summary>
    /// <returns>bool</returns>
    [HttpPut("{id:long:min(1)}", Name = nameof(PutAsync))]
    [ProducesResponseType(typeof(WeatherForecast), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutAsync(long id, WeatherForecast request)
    {
      var forecastReq = MapRequestObject<BusinessLogic.WeatherForecast>(request, out Response response);

      if (response != null || request == null)
        return BadRequest(response);

      var res = await _wfs.GetByIdAsync(id);
      if (res == null)
        return NotFound();

      res = await _wfs.UpdateAsync(id, forecastReq);

      return Ok(MapResponseObject<WeatherForecast>(res));
    }

    /// <summary>
    /// Deletes a WeatherForecast.
    /// </summary>
    /// <returns>bool</returns>
    [HttpDelete("{id:long:min(1)}", Name = nameof(DeleteByIdAsync))]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteByIdAsync(long id)
    {
      var res = await _wfs.GetByIdAsync(id);
      if (res == null)
        return NotFound();

      var boolRes = await _wfs.DeleteAsync(id);

      return Ok(boolRes);
    }

    #region Private methods
    private T MapRequestObject<T>(object source, out Response response)
    {
      response = null;
      var mappedObj = default(T);

      try
      {
        mappedObj = _mapper.Map<T>(source);
      }
      catch (AutoMapperMappingException ex)
      {
        string errorMessage = ex.Message;
        if (ex.InnerException != null)
          errorMessage = ex.InnerException.Message;

        response = new Response()
        {
          Name = "Input data validation",
          Success = false,
          Description = "Input data validation failed. Please see Data: for more details",
          Error = errorMessage 
        };
      }

      return mappedObj;
    }

    private T MapResponseObject<T>(object source)
    {
      return _mapper.Map<T>(source);
    }
    #endregion
  }
}
