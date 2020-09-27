using Poisn.GraphQL.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YesSql;
using Poisn.GraphQL.Shared.Entities;

namespace Poisn.GraphQL.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> logger;

        private readonly IStore _store;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IStore store)
        {
            this.logger = logger;
            _store = store;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> GetAsync()
        {
            Customer customer = new Customer
            {
                FirstName = "hey"
            };

            using (var session = _store.CreateSession())
            {
                //session.Save(customer);

                var test = await session.Query<Customer>().ListAsync();
            }

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}