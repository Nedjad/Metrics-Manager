using MetricsAgent.DAL;
using MetricsAgent.Model;
using MetricsAgent.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetworkAgentController : ControllerBase
    {
        private INetworkMetricsRepository repository;

        private readonly ILogger<NetworkAgentController> _logger;

        public NetworkAgentController(ILogger<NetworkAgentController> logger, INetworkMetricsRepository repo)
        {
            this.repository = repo;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в NetworkAgentController");
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] NetworkMetrics request)
        {
            repository.Create(new NetworkMetrics
            {
                Time = request.Time,
                Value = request.Value

            });
            return Ok();

        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = repository.GetAll();

            var response = new AllNetworkMetricsResponse()
            {
                Metrics = new List<NetworkMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new NetworkMetricDto { Time = metric.Time, Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }

        [HttpGet("/from/{fromTime}/to/{toTime}")]
        public IActionResult GetNetworkData([FromRoute] DateTimeOffset fromTime,
            [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("NetworkLog");

            return Ok();
        }
    }
}
