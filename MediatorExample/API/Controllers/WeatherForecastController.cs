using Database.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WeeklyReview.Queries;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WeatherForecastController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetWeatherForecast/{days}")]
        public async Task<IActionResult> GetWeek(int days)
        {
            var res = await _mediator.Send(new GetWeatherForecastsQuery(days));
            return res.Match<IActionResult>(
                x => Ok(x), 
                e => BadRequest(e.Message));
        }

        [HttpGet("GetWeatherForecastForToday")]
        public async Task<IActionResult> GetToday()
        {
            var res = await _mediator.Send(new GetWeatherForecastQuery(DateTime.Now));
            return Ok(res);
        }
    }
}
