using AutoMapper;

namespace WebApplication
{
    public class ModelProfile : Profile
    {
        public ModelProfile()
        {
          //responses
          CreateMap<BusinessLogic.WeatherForecast, WebApplication.WeatherForecast>();
          //requests
          CreateMap<WebApplication.WeatherForecast, BusinessLogic.WeatherForecast>();
        }
    }
}
