using Database.Models;
using LanguageExt.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WeeklyReview.Queries;

namespace WeeklyReview.Handlers
{
    internal class GetWeatherForecastHandler : IRequestHandler<GetWeatherForecastQuery, WeatherForecast>
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public Task<WeatherForecast> Handle(GetWeatherForecastQuery request, CancellationToken cancellationToken)
        {
            var forecast = new WeatherForecast
            {
                Date = DateOnly.FromDateTime(request.Date),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };
            return Task.FromResult(forecast);
        }
    }

    internal class GetWeatherForecastsHandler : IRequestHandler<GetWeatherForecastsQuery, Result<IEnumerable<WeatherForecast>>>
    {
        private readonly IMediator _mediator;

        public GetWeatherForecastsHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Result<IEnumerable<WeatherForecast>>> Handle(GetWeatherForecastsQuery request, CancellationToken cancellationToken)
        {
            if(request.Days < 0)
            {
                var error = new ArgumentException($"Value cannot be less that zero", nameof(request.Days));
                return new Result<IEnumerable<WeatherForecast>>(error);
            }

            var forecasts = new List<WeatherForecast>();
            var date = DateTime.Now;
            for (int i = 0; i < request.Days; i++)
            {
                forecasts.Add(await _mediator.Send(new GetWeatherForecastQuery(date.AddDays(-i))));
            }
            return forecasts;
        }
    }
}
