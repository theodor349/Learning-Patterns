using Database.Models;
using LanguageExt.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeeklyReview.Queries
{
    public record GetWeatherForecastQuery(DateTime Date) : IRequest<WeatherForecast>;
    public record GetWeatherForecastsQuery(int Days) : IRequest<Result<IEnumerable<WeatherForecast>>>;
}
