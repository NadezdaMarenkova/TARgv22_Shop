﻿using Nancy.Json;
using Shop.Core.Dto;
using Shop.Core.Dto.OpenWeatherDtos;
using System.Net;
using System.Text.Encodings.Web;
using System.Net;
using Shop.Core.ServiceInterface;

namespace Shop.ApplicationServices.Services
{
    public class WeatherForecastServices : IWeatherForecastServices
    {
        public async Task<OpenWeatherResultDto> OpenWeatherResult(OpenWeatherResultDto dto)
        {
            string idOpenWeather = "2852bc2c399d6c482499b9381cb819ae";
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={dto.City}&units=metric&appid={idOpenWeather}";

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);
                OpenWeatherResponseRootDto weatherResult = new JavaScriptSerializer().Deserialize<OpenWeatherResponseRootDto>(json);

                dto.City = weatherResult.Name;
                dto.Temp = weatherResult.Main.Temp;
                dto.FeelsLike = weatherResult.Main.Feels_like;
                dto.Humidity = weatherResult.Main.Humidity;
                dto.Pressure = weatherResult.Main.Pressure;
                dto.WindSpeed = weatherResult.Wind.Speed;
                dto.Description = weatherResult.Weather[0].Description;
            }

            return dto;
        }
    }
}
